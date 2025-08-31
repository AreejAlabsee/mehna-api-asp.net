using MehnahFinalApi.Data;
using MehnahFinalApi.DTO;
using MehnahFinalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MehnahFinalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorksController : ControllerBase
    {
        private readonly AppicatDbContext _context;

        public WorksController(AppicatDbContext context)
        {
            _context = context;
        }

        // GET: api/Works
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkDto>>> GetWorks()
        {
            var works = await _context.Works.ToListAsync();

            var result = works.Select(work => new WorkDto
            {
                Id = work.Id,
                Description = work.Description,
                Price = work.Price,
                Category = work.Category,
                UserId = work.UserId,
                IsAvailable = work.IsAvailable,
                ContactMethod = work.ContactMethod,
                WorkImagesUrl = string.IsNullOrEmpty(work.WorkImages)
                    ? null
                    : $"{Request.Scheme}://{Request.Host}{work.WorkImages}"
            });

            return Ok(result);
        }

        // GET: api/Works/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkDto>> GetWork(int id)
        {
            var work = await _context.Works.FindAsync(id);
            if (work == null) return NotFound();

            var dto = new WorkDto
            {
                Id = work.Id,
                Description = work.Description,
                Price = work.Price,
                Category = work.Category,
                UserId = work.UserId,
                IsAvailable = work.IsAvailable,
                ContactMethod = work.ContactMethod,
                WorkImagesUrl = string.IsNullOrEmpty(work.WorkImages)
                    ? null
                    : $"{Request.Scheme}://{Request.Host}{work.WorkImages}"
            };

            return Ok(dto);
        }

        // POST: api/Works
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<WorkDto>> PostWork([FromForm] WorkDto dto)
        {
            if (dto.WorkImages == null || dto.WorkImages.Length == 0)
                return BadRequest("يرجى رفع صورة.");

            // تحقق من نوع الصورة
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(dto.WorkImages.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                return BadRequest("صيغة الصورة غير مدعومة.");

            var wwwRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var fileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(wwwRoot, "images", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.WorkImages.CopyToAsync(stream);
            }

            var work = new Work
            {
                Description = dto.Description,
                Price = dto.Price,
                Category = dto.Category,
                WorkImages = "/images/" + fileName,
                UserId = dto.UserId,
                IsAvailable = dto.IsAvailable,
                ContactMethod = dto.ContactMethod,
                CreatedDate = DateTime.UtcNow
            };

            _context.Works.Add(work);
            await _context.SaveChangesAsync();

            dto.Id = work.Id;
            dto.WorkImagesUrl = $"{Request.Scheme}://{Request.Host}{work.WorkImages}";
            return CreatedAtAction(nameof(GetWork), new { id = work.Id }, dto);
        }

        // PUT: api/Works/5
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PutWork(int id, [FromForm] WorkDto dto)
        {
            var work = await _context.Works.FindAsync(id);
            if (work == null) return NotFound();

            work.Description = dto.Description;
            work.Price = dto.Price;
            work.Category = dto.Category;
            work.UserId = dto.UserId;
            work.IsAvailable = dto.IsAvailable;
            work.ContactMethod = dto.ContactMethod;
            work.UpdatedDate = DateTime.UtcNow;

            // تعديل الصورة إن وجدت
            if (dto.WorkImages != null && dto.WorkImages.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(dto.WorkImages.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                    return BadRequest("صيغة الصورة غير مدعومة.");

                var wwwRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var fileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(wwwRoot, "images", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.WorkImages.CopyToAsync(stream);
                }

                work.WorkImages = "/images/" + fileName;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Works/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWork(int id)
        {
            var work = await _context.Works.FindAsync(id);
            if (work == null) return NotFound();

            _context.Works.Remove(work);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
