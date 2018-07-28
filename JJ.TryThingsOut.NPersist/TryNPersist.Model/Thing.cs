using System;
namespace TryNPersist.Model
{

    public class Thing
    {

        private System.Int32 m_Id;
        private System.String m_Name;

        public virtual System.Int32 Id
        {
            get
            {
                return m_Id;
            }
        }

        public virtual System.String Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }







    }
}
