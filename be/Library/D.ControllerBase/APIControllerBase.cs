using D.ControllerBase.Exceptions;
using D.ControllerBase.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace D.ControllerBase
{
    public class APIControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private readonly IMediator _mediator;
        public APIControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Return file với stream file
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [NonAction]
        protected FileStreamResult FileByStream(Stream fileStream, string fileName)
        {
            string? ext = Path.GetExtension(fileName)?.ToLower();

            return ext switch
            {
                FileTypes.JPG
                or FileTypes.JPEG
                or FileTypes.JFIF
                    => File(fileStream, MimeTypeNames.ImageJpeg),
                FileTypes.PNG => File(fileStream, MimeTypeNames.ImagePng),
                FileTypes.SVG => File(fileStream, MimeTypeNames.ImageSvgXml),
                FileTypes.GIF => File(fileStream, MimeTypeNames.ImageGif),
                FileTypes.MP4 => File(fileStream, MimeTypeNames.VideoMp4),
                FileTypes.PDF => File(fileStream, MimeTypeNames.ApplicationPdf),
                FileTypes.WEBP => File(fileStream, MimeTypeNames.ImageWebp),
                _ => File(fileStream, MimeTypeNames.ApplicationOctetStream, fileName),
            };
        }

        /// <summary>
        /// Return file với byte file
        /// </summary>
        /// <param name="fileByte"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [NonAction]
        protected FileContentResult FileByFormat(byte[] fileByte, string fileName)
        {
            string? ext = Path.GetExtension(fileName)?.ToLower();

            return ext switch
            {
                FileTypes.JPG
                or FileTypes.JPEG
                or FileTypes.JFIF
                    => File(fileByte, MimeTypeNames.ImageJpeg),
                FileTypes.PNG => File(fileByte, MimeTypeNames.ImagePng),
                FileTypes.SVG => File(fileByte, MimeTypeNames.ImageSvgXml),
                FileTypes.GIF => File(fileByte, MimeTypeNames.ImageGif),
                FileTypes.MP4 => File(fileByte, MimeTypeNames.VideoMp4),
                FileTypes.PDF => File(fileByte, MimeTypeNames.ApplicationPdf),
                FileTypes.WEBP => File(fileByte, MimeTypeNames.ImageWebp),
                _ => File(fileByte, MimeTypeNames.ApplicationOctetStream, fileName),
            };
        }

        [NonAction]
        public ResponseAPI BadRequest(Exception ex)
        {
            int errorCode = 500;
            string message = ex.Message;
            object? data = null;

            if (ex is UserFriendlyException userFriendlyException)
            {
                errorCode = userFriendlyException.ErrorCode;
                if (userFriendlyException.Message != null)
                {
                    message = userFriendlyException.Message;
                }
                else
                {
                    message = "Lỗi không xác định.";
                }
                if (userFriendlyException.Data != null)
                {
                    data = userFriendlyException.Data;
                }
            }
            else if (ex is DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException != null)
                {
                    message = dbUpdateException.InnerException.Message;
                }
                else
                {
                    message = dbUpdateException.Message;
                }
            }
            else if (ex is InvalidOperationException invalidOperationException)
            {
                if (invalidOperationException.InnerException != null)
                {
                    message = invalidOperationException.InnerException.Message;
                }
                else
                {
                    message = invalidOperationException.Message;
                }
            }
            return new ResponseAPI(ControllerBase.StatusCode.Error, data, errorCode, message);

        }
    }
}
