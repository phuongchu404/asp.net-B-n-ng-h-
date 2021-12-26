namespace ThuNghiemLan7.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [Key]
        [StringLength(20)]
        public string MaNhanVien { get; set; }

        
        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(50)]
        public string MatKhau { get; set; }

        [Required]
        [StringLength(50)]
        public string TenNhanVien { get; set; }

        [Required]
        [StringLength(20)]
        public string MaChucVu { get; set; }

        public virtual ChucVu ChucVu { get; set; }
    }
}
