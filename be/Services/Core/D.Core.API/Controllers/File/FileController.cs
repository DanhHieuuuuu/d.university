using D.ControllerBase;
using D.Core.Domain.Dtos.File;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace D.Core.API.Controllers.File
{
    [Route("/api/file")]
    [ApiController]
    public class FileController : APIControllerBase
    {
        private IMediator _mediator;

        public FileController(IMediator mediator)
            : base(mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Danh sách file
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("find")]
        public async Task<ResponseAPI> GetListFile(FileRequestDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Lấy thông tin file theo Id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<ResponseAPI> GetFileById([FromQuery] GetFileByIdDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Tạo mới file và upload lên MinIO
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ResponseAPI> CreateFile([FromForm] CreateFileDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Cập nhật file và upload lên MinIO
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<ResponseAPI> UpdateFile([FromForm] UpdateFileDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Xóa file
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<ResponseAPI> Delete(DeleteFileDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Lấy danh sách file log
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("list-files")]
        public async Task<ResponseAPI> GetLogFiles([FromQuery] GetLogFilesDto dto)
        {
            try
            {
                var result = await _mediator.Send(dto);
                return new(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
