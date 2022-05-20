namespace Shop.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ReplyController : Controller
{
    private readonly IRepository<Reply> _replyRepository;
    private readonly IWorkContext _workContext;

    public ReplyController(IRepository<Reply> replyRepository, IWorkContext workContext)
    {
        _replyRepository = replyRepository;
        _workContext = workContext;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddReply(ReplyForm model)
    {
        if (ModelState.IsValid)
        {
            var user = await _workContext.GetCurrentUser();
            model.ReplierName = user.FullName;

            var reply = new Reply
            {
                ReviewId = model.ReviewId,
                UserId = user.Id,
                Comment = model.Comment,
                ReplierName = user.FullName
            };

            _replyRepository.Add(reply);
            _replyRepository.SaveChanges();

            return PartialView("_ReplyFormSuccess", model);
        }

        return PartialView("_ReplyForm", model);
    }
}

