using Microsoft.AspNetCore.Mvc;
using NotificationService.Domain.Store;
using static Domain.Constant.Constants;

namespace Domain.Common
{
    public class CustomControllerBase : ControllerBase
    {
        public IActionResult OkResponse<T>(T? data)
        {
            var rs = new BaseResponse<T>(
                statusCode: StatusCodeHelper.OK,
                code: ResponseCodeConstants.SUCCESS,
                data: data,
                message: MessagesConstants.SUCCESS
            );
            return base.Ok(rs);
        }

        [NonAction]
        public IActionResult BadRequestResponse(string message)
        {
            var rs = new BaseResponse<string>(
                statusCode: StatusCodeHelper.BadRequest,
                code: ResponseCodeConstants.BADREQUEST,
                message: message
            );
            return base.BadRequest(rs);
        }
    }
}
