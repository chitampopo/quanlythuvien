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
    
    public partial class NHAN_VIEN
    {
        public NHAN_VIEN()
        {
            this.GIU_CHUC_VU = new HashSet<GIU_CHUC_VU>();
            this.MUON_TRA_SACH = new HashSet<MUON_TRA_SACH>();
            this.TAI_KHOAN = new HashSet<TAI_KHOAN>();
        }
    
        public int MA_NHAN_VIEN { get; set; }
        public string TEN_NHAN_VIEN { get; set; }
        public string DIA_CHI_NHAN_VIEN { get; set; }
        public string SDT_NHAN_VIEN { get; set; }
        public Nullable<bool> GIOI_TINH_NHAN_VIEN { get; set; }
    
        public virtual ICollection<GIU_CHUC_VU> GIU_CHUC_VU { get; set; }
        public virtual ICollection<MUON_TRA_SACH> MUON_TRA_SACH { get; set; }
        public virtual ICollection<TAI_KHOAN> TAI_KHOAN { get; set; }
    }
}
