namespace D.Core.Domain.Dtos.Hrm.QuyetDinh
{
    public class NsQuyetDinhLogResponseDto
    {
        public int Id { get; set; }
        public int IdQuyetDinh { get; set; }
        public int? OldStatus { get; set; }
        public int? NewStatus { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
