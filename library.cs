using System;
using System.Collections.Generic;
using System.Linq;

namespace Spring22_HW1
{
    public enum GENDER
    {
        Male,
        Female

    }


    public class HW1_Driver
    {
        public static void Main()
        {
            DateTime dt1 = new DateTime(2015, 12, 20);
            DateTime dt2 = new DateTime(2012, 05, 23);
            Contact C1 = new Contact("Alaa", "Jamal", "07980333", "alaan@gmail.com", "Amman", dt1, GENDER.Female);
            Contact C2 = new Contact("Nada", "Ali", "07898768", "sh@gmail.com", "irbid", dt2, GENDER.Male);

            AddressBook AddressBook1 = new AddressBook("MyBook", 20);
            AddressBook AddressBook2 = new AddressBook("MyBook", 20);
               bool B = false;
            B = AddressBook1.AddContact(C1);
                Console.WriteLine(B);
            //     B = AddressBook1.RemoveContact(C1);//true 
            //    Console.WriteLine(B);
            //   B = AddressBook1.RemoveContact(C1); //true
            //    Console.WriteLine(B);
            //Contact[] Cs = new Contact[] { C1, C2 };//   false
            //  bool[] Bs = AddressBook1.AddContacts(Cs);
            //  int v= AddressBook1.RemoveAllContacts(Cs);


            AddressBook1.AddContact(C1);
            AddressBook1.AddContact(C2);


            //bool ans = false;
            // ans = AddressBook1.UpdateContact(C1);

            AddressBook1.SortContactsByLastName();
            Console.WriteLine(AddressBook1.ToString());

            //Console.WriteLine(ans);
          //  Console.WriteLine(AddressBook1.Equals(AddressBook2));


           // Console.WriteLine(AddressBook1.ToString() + "\n");
           // AddressBook1.SortContacts(C1.FirstName);
           // Console.WriteLine(AddressBook1.ToString());





        }
    }



    public class AddressBook
    {
        public string Name { get; set; }
        public int Size { get; set; }


        //private
        List<Contact> AddressBookList = new List<Contact>();



        public AddressBook() : this("MyBook", 10) //use chaining constructor. default Size is 10 and Name is "MyBook"
        {

            AddressBookList = new List<Contact>();

        }

        public AddressBook(string Name, int Size)
        {
            this.Name = Name;
            this.Size = Size;
            AddressBookList = new List<Contact>(this.Size);
        }

        public override string ToString()
        //print this address book, one contact at a line with a header
        {
            Console.WriteLine($"bookname : {Name}  Size : {Size} \n");
            foreach (Contact C in AddressBookList)
            {

                Console.WriteLine(C.ToString());

            }
            return "";



        }


        public override bool Equals(object obj)
        {
            bool flag = true;

          

            AddressBook that = obj as AddressBook;
            if (that == null)
                return false;

            if (this.AddressBookList.Count != that.AddressBookList.Count)
                return false;

            for (int i = 0; i < this.AddressBookList.Count; i++)
            {
                if (!this.AddressBookList.ElementAt(i).Equals(that.AddressBookList.ElementAt(i)))
                {
                    return false;
                    flag = false;
                }

            }

            return this.Name.Equals(that.Name) && this.Size.Equals(that.Size)
               && flag;


        }


        public bool AddContact(Contact C)
        //Add a new contact and returns true if successfully added
        {
            bool isNot = AddressBookList.Contains(C);

            if (isNot == false && C != null)
            {
                AddressBookList.Add(C);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool[] AddContacts(Contact[] Cs)
        //Add all contacts in the array and returns array of booleans indicating
        //if the contact in the specific index has been added or not
        {
            bool[] ans = new bool[Cs.Length];
            for (int i = 0; i < Cs.Length; i++)
            {
                if (AddContact(Cs[i]))
                    ans[i] = true;
                else
                    ans[i] = false;

            }
            return ans;

        }

        public bool RemoveContact(Contact C)
        //remove the given contact if exists and returns true, otherwise, return false
        {


            if (AddressBookList.Contains(C))
            {
                int ind = AddressBookList.IndexOf(C);
                AddressBookList.RemoveAt(ind);
                return true;
            }
            else
            {
                return false;
            }

        }
        public int RemoveAllContacts(Contact[] Cs)
        //remove all contacts from this AddressBook that are in the given array and return the
        //number of removed contacts.
        {
            bool[] ans = new bool[Cs.Length];
            int count = 0;
            for (int i = 0; i < Cs.Length; i++)
            {
                if (RemoveContact(Cs[i]))
                {
                    ans[i] = true;
                    count++;
                }
                else ans[i] = false;

            }
            return count;


        }


        public bool UpdateContact(Contact C)
        //update the given contact of exist, use Equals method to compare
        {
            
            foreach (Contact Con in AddressBookList)
            {
                if (Con.Equals(C))
                {
                    C.Address = "UAE";
                    return true;

                }
            }
            
            
                return false;
            


        }




        public Contact[] ExportContacts()
        {
            Contact[] Contacts = new Contact[AddressBookList.Count];

            for (int i = 0; i < Contacts.Length; i++)
            {
                Contacts[i] = AddressBookList[i];

            }
            return Contacts;
        }






        public bool CopyContacts(AddressBook addressBook)
        //add all contact from the given address book to this address book. After you copy the
        // contacts make sure there is no connection between the two objects.
        {

            List<Contact> Contacts = addressBook.AddressBookList;

            bool flag = false;
            foreach (Contact C in Contacts)
            {
                flag = this.AddContact(C);

            }
            return flag;




        }

        public int Count()
        //number of contacts in this address book
        {
            return AddressBookList.Count;

        }
        public bool IsFull()
        {
            return (AddressBookList.Count == Size);

        }
        public bool IsEmpty()
        {
            return (AddressBookList.Count == 0);

        }




        public Contact GetContactAt(int Idx)
        //returns the contact at the given index
        {
            return AddressBookList.ElementAt(Idx);
        }



        public void SortContactsByLastName()//Bonus
        {
            AddressBookList.Sort();

        }

        //sort all contacts based on their last name
        public void SortContacts(IComparer<Contact> MyComparer) //Bonus
                                                                //sort all contacts using the given IComparer object
        {
            MyComparer = new myComparerContact();
            AddressBookList.Sort(MyComparer);
        }




    }

    public class myComparerContact : IComparer<Contact>
    {
        public int Compare(Contact x, Contact y)
        {
            if (x.FirstName.CompareTo(y.FirstName) == 0)
                return x.LastName.CompareTo(y.LastName);
            return x.FirstName.CompareTo(y.FirstName);
        }

    }

    public class Contact : IComparable<Contact>
    {
        //all of the following instance variables should be implemented as private instance variables
        //and public properties


        private string _FirstName;
        public string FirstName
        {
            get
            {
                return string.Format("({0})", this._FirstName);
            }
            set
            {
                this._FirstName = value;
            }

        }


        private string _LastName;
        public string LastName
        {
            get
            {
                return string.Format("({0})", this._LastName);
            }
            set
            {
                this._LastName = value;
            }

        }

        private string _PhoneNumber;
        public string PhoneNumber
        {
            get
            {
                return string.Format("({0})", this._PhoneNumber);
            }
            set
            {
                this._PhoneNumber = value;
            }

        }


        private string _Email;
        public string Email
        {
            get
            {
                return string.Format("({0})", this._Email);
            }
            set
            {
                this._Email = value;
            }

        }


        private string _Address;
        public string Address
        {
            get
            {
                return string.Format("({0})", this._Address);
            }
            set
            {
                this._Address = value;
            }

        }

        private string _DoB;
        public string DoB
        {
            get
            {
                return this._DoB;
            }
            set
            {
                this._DoB = value;
            }

        }




        private string _Gender;
        public string Gender
        {
            get
            {
                return this._Gender;
            }
            set
            {
                this._Gender = value;
            }

        }



        //custom constructor with all variables

        public Contact(string FirstName, string LastName, string PhoneNumber, string Email, string Address, DateTime DoB, GENDER Gender)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.Address = Address;
            this.DoB = DoB.ToString();
            this.Gender = Gender.ToString();

        }




        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}, {5}", this._LastName, this._FirstName, this._PhoneNumber, this._Email, this._Address, this._DoB);

        }
        //print this contact object in this comma separated values format:
        // Last Name, First Name, PhoneNumber, Email, Address, DoB
        public override bool Equals(object obj)
        {

          
            Contact that = obj as Contact;

            if (that == null)
                return false;

            else 
                return this._FirstName.Equals(that._FirstName) && this._LastName.Equals(that._LastName)
                     && this._PhoneNumber.Equals(that._PhoneNumber)
                     && this._Email.Equals(that._Email)
                     && this._Address.Equals(that._Address)
                     && this._DoB.Equals(that._DoB)
                     && this._Gender.Equals(that._Gender);
           


        }


        public override int GetHashCode()
        {
            return _FirstName.GetHashCode() * _LastName.GetHashCode() * _PhoneNumber.GetHashCode() * _Address.GetHashCode() * _Email.GetHashCode() * _DoB.GetHashCode();
        }



        public int CompareTo(Contact other)
        {
            Contact that = other as Contact;
            if (that == null || other == null)
                throw new ArgumentException();

            if (this._LastName.CompareTo(other._LastName) == 0)
                return this._FirstName.CompareTo(other._FirstName);
            return this._LastName.CompareTo(other._LastName);



        }





    }
}