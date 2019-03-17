using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Models
{
    public class AuditInfo
    {
        public List<TB_UserAudit> tB_UserAudits;
        public List<TB_CarAudit> tB_CarAudits;
    }
}