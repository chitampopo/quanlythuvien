//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoHoiCuoiCung.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MUON_TRA_SACH
    {
        public MUON_TRA_SACH()
        {
            this.PHATs = new HashSet<PHAT>();
            this.SACH_MUON = new HashSet<SACH_MUON>();
        }
    
        public int MA_MUON_TRA { get; set; }
        public int MA_CHINH_SACH { get; set; }
        public int MA_DOC_GIA { get; set; }
        public int MA_NHAN_VIEN { get; set; }
        public Nullable<System.DateTime> NGAY_GIO_LAP { get; set; }
        public Nullable<System.DateTime> NGAY_PHAI_TRA { get; set; }
    
        public virtual CHINH_SACH_LUU_THONG CHINH_SACH_LUU_THONG { get; set; }
        public virtual DOC_GIA DOC_GIA { get; set; }
        public virtual NHAN_VIEN NHAN_VIEN { get; set; }
        public virtual ICollection<PHAT> PHATs { get; set; }
        public virtual ICollection<SACH_MUON> SACH_MUON { get; set; }
    }
}