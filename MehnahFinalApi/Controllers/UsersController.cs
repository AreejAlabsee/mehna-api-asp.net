using MehnahFinalApi.Data;
using MehnahFinalApi.DTO;
using MehnahFinalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MehnahFinalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppicatDbContext _context;
        private readonly IWebHostEnvironment _env;

        public UsersController(AppicatDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PutUser(int id, [FromForm] UserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            user.Name = dto.Name;
            user.PhoneNumber = dto.PhoneNumber;
            user.Password = dto.Password;
            user.UserType = dto.UserType;

            if (dto.ProfileImage != null && dto.ProfileImage.Length > 0)
            {
                // حذف الصورة القديمة إن وجدت
                if (!string.IsNullOrWhiteSpace(user.ProfileImage))
                {
                    var oldImagePath = Path.Combine(_env.WebRootPath, user.ProfileImage.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);
                }

                // حفظ الصورة الجديدة
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.ProfileImage.FileName)}";
                var folderPath = Path.Combine(_env.WebRootPath, "images");
                Directory.CreateDirectory(folderPath);
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ProfileImage.CopyToAsync(stream);
                }

                user.ProfileImage = "/images/" + fileName;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<User>> PostUser([FromForm] UserDto dto)
        {
            if (dto.ProfileImage == null || dto.ProfileImage.Length == 0)
                return BadRequest("يجب رفع صورة الملف الشخصي.");

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.ProfileImage.FileName)}";
            var folderPath = Path.Combine(_env.WebRootPath, "images");
            Directory.CreateDirectory(folderPath);
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.ProfileImage.CopyToAsync(stream);
            }

            var user = new User
            {
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                Password = dto.Password,
                UserType = dto.UserType,
                ProfileImage = "/images/" + fileName
               
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Works)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound();

            // حذف الصورة الشخصية
            if (!string.IsNullOrWhiteSpace(user.ProfileImage))
            {
                var imagePath = Path.Combine(_env.WebRootPath, user.ProfileImage.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }

            // حذف الأعمال المرتبطة
            if (user.Works.Any())
                _context.Works.RemoveRange(user.Works);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }
    }
}
