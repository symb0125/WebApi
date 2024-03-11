using WebApi.Dtos;

public interface IUserService
{

    Task<ServiceResponse<List<User>>> GetAllUsers();
    Task<ServiceResponse<User>> GetUserById(int id);
    Task<ServiceResponse<List<User>>> AddUser(AddUserDto newUser);
    Task<ServiceResponse<User>> UpdateUser(AddUserDto updatedUser);
    Task<ServiceResponse<User>> DeleteUserById(int id);
}