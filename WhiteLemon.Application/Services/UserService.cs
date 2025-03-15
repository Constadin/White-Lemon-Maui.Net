using Microsoft.AspNetCore.Identity;
using WhiteLemon.API.Models;
using WhiteLemon.Application.DTOs;
using WhiteLemon.Application.Interfaces;
using WhiteLemon.Domain.Entities;
using WhiteLemon.Domain.Interfaces;

namespace WhiteLemon.Application.Services
{
    /// <summary>
    /// Provides user-related services, including registration and login functionality.
    /// Παρέχει υπηρεσίες που σχετίζονται με τον χρήστη, όπως η εγγραφή και η σύνδεση.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// Αρχικοποιεί μια νέα περίπτωση της κλάσης <see cref="UserService"/>.
        /// </summary>
        public UserService(IPasswordHasher<User> passwordHasher, IUserRepository userRepository)
        {
            this._passwordHasher = passwordHasher;
            this._userRepository = userRepository;
        }

        /// <summary>
        /// Registers a new user in the system asynchronously.
        /// Εγγράφει έναν νέο χρήστη στο σύστημα ασύγχρονα.
        /// </summary>
        /// <param name="userDto">The data transfer object containing the user's registration details.</param>
        /// <returns>A service result indicating whether the registration was successful or not.</returns>
        public async Task<ServiceResult<ResponseRegisterUserDto>> RegisterUserAsync(RegisterUserDto userDto)
        {
            // Έλεγχος αν τα πεδία είναι άδεια ή λευκές αλυσίδες χαρακτήρων
            if (string.IsNullOrWhiteSpace(userDto.Email) || string.IsNullOrWhiteSpace(userDto.Password))
            {
                return ServiceResult<ResponseRegisterUserDto>.FailureResult("Invalid user data.");
            }

            var user = await this._userRepository.GetUserByEmailAsync(userDto.Email);

            if(user?.Email == userDto.Email)
            {
                return ServiceResult<ResponseRegisterUserDto>.FailureResult("This user already exists.");
            }
            
            user= new User
            {
                Name = userDto.Name ?? string.Empty,
                Email = userDto.Email ?? string.Empty,
                PhotoUrl = userDto.photoUrl ?? null,
                PhotoPath = userDto.PhotoPath ?? string.Empty,
                CreatedAt = DateTime.UtcNow.AddMilliseconds(-DateTime.UtcNow.Millisecond)
            };

            // Δημιουργία του κωδικού πρόσβασης με hashing
            user.PasswordHash = this._passwordHasher.HashPassword(user, userDto.Password);

            // Προσθήκη του χρήστη στη βάση δεδομένων
            await this._userRepository.AddRegisterAsync(user);

            // Δημιουργία και επιστροφή του ResponseUserDto
            var userDtoResponse = new ResponseRegisterUserDto(user.Id, user.Name, user.Email, user.PhotoUrl);
       

            return ServiceResult<ResponseRegisterUserDto>.SuccessResult(userDtoResponse, "User registered successfully.");
        }

        /// <summary>
        /// Authenticates a user based on their email and password asynchronously.
        /// Αυθεντικοποιεί έναν χρήστη με βάση το email και τον κωδικό πρόσβασης ασύγχρονα.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>A service result containing the user's details and token if authentication is successful.</returns>
        public async Task<ServiceResult<ResponseLoginUserDto>> LoginUserAsync(LoginUserDto userDto)
        {
            // Βρίσκουμε τον χρήστη από τη βάση δεδομένων
            var user = await this._userRepository.GetUserByEmailAsync(userDto.Email!);

            if (user == null) // Αν ο χρήστης δεν υπάρχει, επιστρέφουμε αποτυχία
            {
                return ServiceResult<ResponseLoginUserDto>.FailureResult("Invalid user data.");
            }
               
            // Έλεγχος του κωδικού πρόσβασης με hashing και verification
            var passwordVerificationResult = this._passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userDto.Password!);

            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                return ServiceResult<ResponseLoginUserDto>.FailureResult("Invalid user password.");
            }

            // Επιστρέφουμε τα στοιχεία του χρήστη σε περίπτωση επιτυχίας
            var responseUserDto = new ResponseLoginUserDto(user.Id, user.Email, user.Name);

            return ServiceResult<ResponseLoginUserDto>.SuccessResult(responseUserDto, "User login successfully.");
        }
    }
}
