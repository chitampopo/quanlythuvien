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
    
    public partial class DOC_GIA
    {
        public DOC_GIA()
        {
            this.MUON_TRA_SACH = new HashSet<MUON_TRA_SACH>();
            this.TAI_KHOAN = new HashSet<TAI_KHOAN>();
        }
    
        public int MA_DOC_GIA { get; set; }
        public int MA_DOI_TUONG { get; set; }
        public string TEN_DOC_GIA { get; set; }
        public string SDT_DOC_GIA { get; set; }
        public string DIA_CHI_DOC_GIA { get; set; }
        public string EMAIL { get; set; }
        public Nullable<System.DateTime> NGAY_THAM_GIA { get; set; }
        public Nullable<System.DateTime> NGAY_HET_HAN { get; set; }
        public Nullable<bool> GIOI_TINH_DOC_GIA { get; set; }
        public string CMND { get; set; }
    
        public virtual DOI_TUONG DOI_TUONG { get; set; }
        public virtual ICollection<MUON_TRA_SACH> MUON_TRA_SACH { get; set; }
        public virtual ICollection<TAI_KHOAN> TAI_KHOAN { get; set; }
    }
}