using MehnahFinalApi.Data;
using MehnahFinalApi.DTO;
using MehnahFinalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MehnahFinalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly AppicatDbContext _context;

        public RatingsController(AppicatDbContext context)
        {
            _context = context;
        }

        // GET: api/Ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingResponseDto>>> GetRatings()
        {
            var ratings = await _context.Ratings
                .Include(r => r.Reviewer)
                .Include(r => r.Work)
                .Select(r => new RatingResponseDto
                {
                    Id = r.Id,
                    Stars = r.Stars,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt,
                    ReviewerName = r.Reviewer.Name,
                    WorkId = r.WorkId
                    // إذا أردت اسم العمل
                }).ToListAsync();

            return ratings;
        }

        // GET: api/Ratings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RatingResponseDto>> GetRating(int id)
        {
            var rating = await _context.Ratings
                .Include(r => r.Reviewer)
                .Include(r => r.Work)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rating == null)
                return NotFound();

            var result = new RatingResponseDto
            {
                Id = rating.Id,
                Stars = rating.Stars,
                Comment = rating.Comment,
                CreatedAt = rating.CreatedAt,
                ReviewerName = rating.Reviewer.Name,
                WorkId=rating.WorkId
              
            };

            return result;
        }

        // POST: api/Ratings
        [HttpPost]
        public async Task<ActionResult<RatingResponseDto>> PostRating(RatingDto dto)
        {
            var work = await _context.Works.FindAsync(dto.WorkId);
            var reviewer = await _context.Users.FindAsync(dto.ReviewerId);

            if (work == null || reviewer == null)
                return BadRequest("العمل أو المستخدم غير موجود");

            var rating = new Rating
            {
                Stars = dto.Stars,
                Comment = dto.Comment,
                WorkId = dto.WorkId,
                ReviewerId = dto.ReviewerId,
                //ReviewerName=dto.ReviewerName,
                CreatedAt = DateTime.UtcNow
            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            // إعادة DTO فقط لتجنب دورة JSON
            var response = new RatingResponseDto
            {
                Id = rating.Id,
                Stars = rating.Stars,
                Comment = rating.Comment,
                CreatedAt = rating.CreatedAt,
                ReviewerName = reviewer.Name,
              
            };

            return CreatedAtAction(nameof(GetRating), new { id = rating.Id }, response);
        }

        // PUT: api/Ratings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(int id, RatingDto dto)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null) return NotFound();

            var work = await _context.Works.FindAsync(dto.WorkId);
            var reviewer = await _context.Users.FindAsync(dto.ReviewerId);

            if (work == null || reviewer == null)
                return BadRequest("العمل أو المستخدم غير موجود");

            rating.Stars = dto.Stars;
            rating.Comment = dto.Comment;
            rating.WorkId = dto.WorkId;
            rating.ReviewerId = dto.ReviewerId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null) return NotFound();

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}








//using MehnahFinalApi.Data;
//using MehnahFinalApi.DTO;
//using MehnahFinalApi.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//namespace MehnahFinalApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RatingsController : ControllerBase
//    {
//        private readonly AppicatDbContext _context;

//        public RatingsController(AppicatDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Ratings
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<RatingResponseDto>>> GetRatings()
//        {
//            var ratings = await _context.Ratings
//                .Include(r => r.Reviewer)
//                .Include(r => r.Work)
//                .Select(r => new RatingResponseDto
//                {
//                    Id = r.Id,
//                    Stars = r.Stars,
//                    Comment = r.Comment,
//                    CreatedAt = r.CreatedAt,
//                    ReviewerName = r.Reviewer.Name,

//                    ReviewerImage = r.Reviewer.ProfileImage
//                }).ToListAsync();

//            return ratings;
//        }

//        // POST: api/Ratings
//        [HttpPost]
//        public async Task<ActionResult<Rating>> PostRating(RatingDto dto)
//        {
//            // التأكد من وجود العمل والمستخدم
//            var work = await _context.Works.FindAsync(dto.WorkId);
//            var reviewer = await _context.Users.FindAsync(dto.ReviewerId);

//            if (work == null || reviewer == null)
//                return BadRequest("العمل أو المستخدم غير موجود");

//            var rating = new Rating
//            {
//                Stars = dto.Stars,
//                Comment = dto.Comment,
//                WorkId = dto.WorkId,
//                ReviewerId = dto.ReviewerId,
//                CreatedAt = DateTime.UtcNow
//            };

//            _context.Ratings.Add(rating);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetRating), new { id = rating.Id }, rating);
//        }

//        // GET: api/Ratings/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<RatingResponseDto>> GetRating(int id)
//        {
//            var rating = await _context.Ratings
//                .Include(r => r.Reviewer)
//                .Include(r => r.Work)
//                .FirstOrDefaultAsync(r => r.Id == id);

//            if (rating == null)
//                return NotFound();

//            var result = new RatingResponseDto
//            {
//                Id = rating.Id,
//                Stars = rating.Stars,
//                Comment = rating.Comment,
//                CreatedAt = rating.CreatedAt,
//                ReviewerName = rating.Reviewer.Name,

//                ReviewerImage = rating.Reviewer.ProfileImage
//            };

//            return result;
//        }
//        // PUT: api/Ratings/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutRating(int id, RatingDto dto)
//        {
//            var rating = await _context.Ratings.FindAsync(id);
//            if (rating == null)
//            {
//                return NotFound();
//            }

//            // تحقق من وجود العمل والمستخدم أيضاً
//            var work = await _context.Works.FindAsync(dto.WorkId);
//            var reviewer = await _context.Users.FindAsync(dto.ReviewerId);

//            if (work == null || reviewer == null)
//                return BadRequest("العمل أو المستخدم غير موجود");

//            // تحديث الحقول
//            rating.Stars = dto.Stars;
//            rating.Comment = dto.Comment;
//            rating.WorkId = dto.WorkId;
//            rating.ReviewerId = dto.ReviewerId;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!_context.Ratings.Any(e => e.Id == id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }


//        // DELETE: api/Ratings/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteRating(int id)
//        {
//            var rating = await _context.Ratings.FindAsync(id);
//            if (rating == null)
//                return NotFound();

//            _context.Ratings.Remove(rating);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }
//    }
//}
