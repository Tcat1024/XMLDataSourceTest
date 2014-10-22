using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Access
{
   public class KRBaseInfo
    {
       public string ColName;
       public string ColValue;        
    }
   public class KRKeyEnevts
   {
       public DateTime DateAndTime;
       public float Duration;
       public string Descripion;
       public float Weight;
       public Int32 Tempture;
       public string Ele_C;
       public string Ele_Si;
       public string Ele_Mn;
       public string Ele_S;
       public string Ele_P;
       public string Ele_Cu;
       public string Ele_As;
       public string Ele_Sn;
       public string Ele_Cu5As8Sn;
       public string Ele_Cr;
       public string Ele_Ni;
       public string Ele_Mo;
       public string Ele_Ti;
       public string Ele_Nb;
       public string Ele_Pb;
   }

   public class KRMixerHeightSpeed
   {
       public DateTime DateAndTime;
       public float sngDuration;
       public string DateTimeDuration;
       public float Height;
       public float Speed;
   }
}
