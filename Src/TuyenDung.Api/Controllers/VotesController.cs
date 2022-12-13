using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuyenDung.Common.Extensions;
using TuyenDung.Data.EF;
using TuyenDung.Data.Entities;
using TuyenDung.Model;

namespace TuyenDung.Api.Controllers
{
    public class VotesController : BaseController
    {
        private readonly TuyenDungDbContext _context;

        public VotesController(TuyenDungDbContext context)
        {
            _context = context;
        }

        #region Votes

        [HttpGet("{knowledgeBaseId}/votes")]
        public async Task<IActionResult> GetVotes(int knowledgeBaseId)
        {
            var votes = await _context.Votes
                .Where(x => x.KnowledgeBaseId == knowledgeBaseId)
                .Select(x => new VoteModel()
                {
                    UserId = x.UserId,
                    KnowledgeBaseId = x.KnowledgeBaseId,
                    CreateDate = x.CreateDate,
                    LastModifiedDate = x.LastModifiedDate
                }).ToListAsync();

            return Ok(votes);
        }

        [HttpPost("{knowledgeBaseId}/votes")]
        public async Task<IActionResult> PostVote(int knowledgeBaseId)
        {
            var claims = HttpContext.User.Claims;
            var userId = claims.FirstOrDefault(c => c.Type == "Id").Value;
            var knowledgeBase = await _context.KnowledgeBases.FindAsync(knowledgeBaseId);
            if (knowledgeBase == null)
                return BadRequest(new ApiBadRequestResponse($"Cannot found knowledge base with id {knowledgeBaseId}"));

            var numberOfVotes = await _context.Votes.CountAsync(x => x.KnowledgeBaseId == knowledgeBaseId);
            var vote = await _context.Votes.FindAsync(knowledgeBaseId, userId);
            if (vote != null)
            {
                _context.Votes.Remove(vote);
                numberOfVotes -= 1;
            }
            else
            {
                vote = new Vote()
                {
                    KnowledgeBaseId = knowledgeBaseId,
                    UserId = userId
                };
                _context.Votes.Add(vote);
                numberOfVotes += 1;
            }

            knowledgeBase.NumberOfVotes = numberOfVotes;
            _context.KnowledgeBases.Update(knowledgeBase);

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok(numberOfVotes);
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse($"Vote failed"));
            }
        }

        [HttpDelete("{knowledgeBaseId}/votes/{userId}")]
        public async Task<IActionResult> DeleteVote(int knowledgeBaseId, string userId)
        {
            var vote = await _context.Votes.FindAsync(knowledgeBaseId, userId);
            if (vote == null)
                return NotFound(new ApiNotFoundResponse("Cannot found vote"));

            var knowledgeBase = await _context.KnowledgeBases.FindAsync(knowledgeBaseId);
            if (knowledgeBase != null)
                return BadRequest(new ApiBadRequestResponse($"Cannot found knowledge base with id {knowledgeBaseId}"));

            knowledgeBase.NumberOfVotes = knowledgeBase.NumberOfVotes.GetValueOrDefault(0) - 1;
            _context.KnowledgeBases.Update(knowledgeBase);

            _context.Votes.Remove(vote);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok();
            }
            return BadRequest(new ApiBadRequestResponse($"Delete vote failed"));
        }

        #endregion Votes
    }
}