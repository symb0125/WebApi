
using Microsoft.Win32.SafeHandles;
using WebApi.Dtos;

public class UserService : IUserService
{
    private static User defaultUser = new User();
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public UserService(IMapper mapper, DataContext context)
    {
        _context = context;//useToInteractWithDB
        _mapper = mapper;//useToConvertModelsIntoDTO
    }

    public async Task<ServiceResponse<List<User>>> GetAllUsers()
    {
        ServiceResponse<List<User>> sr = new ServiceResponse<List<User>>();
        var listUser = await _context.userTable.ToListAsync();
        sr.data = listUser;
        if (sr.data == null || sr.data.Count == 0) { sr.message = "List is Empty."; }
        return sr;
    }

    public async Task<ServiceResponse<User>> GetUserById(int id)
    {
        ServiceResponse<User> sr = new ServiceResponse<User>();
        var listUser = await _context.userTable.ToListAsync();
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
            _context.userTable.Add(user);
            await _context.SaveChangesAsync();
            sr.data = await _context.userTable.ToListAsync();
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
            var listUser = await _context.userTable.ToListAsync();
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
            if (oldUser is null)
            {
                throw new Exception($"User with Id {updatedUser.Id} Not Found.");
            }
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            sr.success = false;
            sr.message = ex.Message;
        }
        return sr;
    }

    public async Task<ServiceResponse<User>> DeleteUserById(int id)
    {
        ServiceResponse<User> sr = new ServiceResponse<User>();
        try
        {
            User? deletedUser = await _context.userTable.FirstOrDefaultAsync(u => u.Id == id);
            if (deletedUser is null)
                throw new Exception($"User with Id {id} doesn't Exists.");

            _context.userTable.Remove(deletedUser);
            await _context.SaveChangesAsync();
            sr.data = deletedUser;
        }
        catch (Exception ex)
        {
            sr.success = false;
            sr.message = ex.Message;
        }
        return sr;
    }
}