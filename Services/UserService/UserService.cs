
using Microsoft.Win32.SafeHandles;
using WebApi.Dtos;

public class UserService : IUserService
{
    private static User defaultUser = new User();
    private static List<User> listUser = new List<User>{
            new User{Id=0, name = "pratik"},
            new User{Id=1, name = "symb"}
        };

    private readonly IMapper _mapper;
    public UserService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<ServiceResponse<User>> DeleteUserById(int id)
    {
        ServiceResponse<User> sr = new ServiceResponse<User>();
        User? deletedUser = null;
        foreach (User currUser in listUser)
        {
            if (currUser.Id == id)
            {
                deletedUser = currUser;
            }
        }
        listUser.Remove(deletedUser);
        sr.data = deletedUser;
        return sr;
    }

    public async Task<ServiceResponse<List<User>>> GetAllUsers()
    {
        ServiceResponse<List<User>> sr = new ServiceResponse<List<User>>();
        if (sr.data == null || sr.data.Count == 0) sr.message = "List is Empty.";
        sr.data = listUser;
        return sr;
    }

    public async Task<ServiceResponse<User>> GetUserById(int id)
    {
        ServiceResponse<User> sr = new ServiceResponse<User>();
        try
        {
            bool isFound = false;
            foreach (User currUser in listUser)
            {
                if (currUser.Id == id)
                {
                    sr.data = currUser;
                    isFound = !isFound;
                    break;
                }
            }
            if (isFound)
            {
                throw new Exception($"User with Id {id} Not Found.");
            }
        }
        catch (Exception ex)
        {
            sr.success = false;
            sr.message = ex.Message;
        }
        return sr;
    }

    public async Task<ServiceResponse<List<User>>> AddUser(AddUserDto newUser)
    {
        ServiceResponse<List<User>> sr = new ServiceResponse<List<User>>();
        try
        {
            var user = _mapper.Map<User>(newUser);
            listUser.Add(user);
            sr.data = listUser;
        }
        catch (Exception ex)
        {
            sr.success = false;
            sr.message = ex.Message;
        }
        return sr;
    }

    public async Task<ServiceResponse<User>> UpdateUser(AddUserDto updateUser)
    {
        ServiceResponse<User> sr = new ServiceResponse<User>();
        try
        {
            User? oldUser = null;
            var updatedUser = _mapper.Map<User>(updateUser);
            foreach (User currUser in listUser)
            {
                if (currUser.Id == updatedUser.Id)
                {
                    if (currUser.Equals(updatedUser))
                    {
                        // return BadRequest("Same Data Provided.");
                    }
                    oldUser = currUser;
                    break;
                }
            }
            if (oldUser != null)
            {
                oldUser = new User(updatedUser);
                sr.data = updatedUser;
            }
            else
            {
                throw new Exception($"User with Id {updatedUser.Id} Not Found.");
            }
        }
        catch (Exception ex)
        {
            sr.success = false;
            sr.message = ex.Message;
        }
        return sr;
    }
}