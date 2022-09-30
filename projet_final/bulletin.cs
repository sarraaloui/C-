using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_final
{
    class bulletin
    {

        string num;
        public string Num
        {
            get { return num; }

            set { num = value; }

        }



        string date_depot;
        public string Date_depot
        {
            get { return date_depot; }

            set { date_depot = value; }

        }
        string acte_effectue;
        public string Acte_effectue
        {
            get { return acte_effectue; }

            set { acte_effectue = value; }

        }
        float frais_acte;
        public float Frais_acte
        {
            get { return frais_acte; }

            set { frais_acte = value; }

        }

        employee employe ;
        


    }
}
