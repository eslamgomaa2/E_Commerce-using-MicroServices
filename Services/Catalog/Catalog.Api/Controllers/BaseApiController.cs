using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // 1. حطينا القوسين المربعين [controller] وسينا الـ V كابيتال في apiVersion
    [ApiController]
    public class BaseApiController : ControllerBase
    {

    }
}
