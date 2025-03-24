using WhiteLemonMauiUI.Users.ModelsDto;

namespace WhiteLemonMauiUI.Mappers
{
    public class UserMapper
    {
        public static RegisterUserRequest ToRegisterUserRequest(string? userName, string? email, string? password, string? photoUser)
        {

            return new RegisterUserRequest()
            {
                Name = userName ?? string.Empty,
                Email = email ?? string.Empty,
                Password = password ?? string.Empty,
                PhotoUser = photoUser ?? string.Empty
            };

        }


        public static LoginUserRequest ToLoginUserRequest(string? email, string? password) =>
            new LoginUserRequest()
            {
                Email = email ?? string.Empty,
                Password = password ?? string.Empty
            };

        public static PostDto ToAddPostUserRequest(Guid id, Guid userId, string title, string content, DateTime createdAt, DateTime? modifiedOn, List<PostImageDto> postImages) =>
                    new(id, userId, title, content, createdAt, modifiedOn, postImages ?? new List<PostImageDto>());

    }
}
