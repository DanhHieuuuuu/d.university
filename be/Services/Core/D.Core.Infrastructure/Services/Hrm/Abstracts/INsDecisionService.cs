using D.Core.Domain.Dtos.Hrm.QuyetDinh;
using D.Core.Domain.Entities.Hrm.NhanSu;
using D.DomainBase.Dto;

namespace D.Core.Infrastructure.Services.Hrm.Abstracts
{
    public interface INsDecisionService
    {
        /// <summary>
        /// Tạo quyết định mới
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public NsQuyetDinh TaoQuyetDinh(CreateNsQuyetDinhDto dto);

        /// <summary>
        /// Phê duyệt quyết định
        /// </summary>
        /// <param name="idQuyetDinh">Id của quyết định</param>
        public void PheDuyetQuyetDinh(int idQuyetDinh);

        /// <summary>
        /// Từ chối quyết định
        /// </summary>
        /// <param name="idQuyetDing">Id của quyết định</param>
        /// <param name="liDo">Lí do từ chối</param>
        public void TuChoiQuyetDinh(int idQuyetDing, string liDo);

        /// <summary>
        /// Danh sách các quyết định
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public PageResultDto<NsQuyetDinhResponseDto> FindPagingNsQuyetDinh(
            NsQuyetDinhRequestDto dto
        );
    }
}
