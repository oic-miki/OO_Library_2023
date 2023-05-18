﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OO_C_Sharp_WinFormsApp
{

    public abstract class PlaceRegister
    {

        private Place place;
        private Status status = NullStatus.get();

        public PlaceRegister(Place place)
        {

            Debug.Assert(place != null);

            this.place = place;

            Debug.Assert(this.place != null);

        }

        public Place getPlace()
        {

            return place;

        }

        public Status getStatus()
        {

            return status;

        }

        public virtual PlaceRegister setStatus(Status status)
        {

            Debug.Assert(status != null);

            this.status = status;

            Debug.Assert(this.status != null);

            return this;

        }

    }

    public class PersonPlaceRegister : PlaceRegister
    {

        private Person person;

        public PersonPlaceRegister(Place place, Person person) : base(place)
        {

            Debug.Assert(person != null);

            this.person = person;

            Debug.Assert(this.person != null);

        }

        public Person getPerson()
        {

            return person;

        }

    }

    public class UserPlaceRegister : PlaceRegister
    {

        private User user;

        public UserPlaceRegister(Place place, User user) : base(place)
        {

            Debug.Assert(user != null);

            this.user = user;

            Debug.Assert(this.user != null);

        }

        public User getUser()
        {

            return user;

        }

    }

    public class NullPersonPlaceRegister : PersonPlaceRegister, NullObject
    {
        
        private static PersonPlaceRegister personPlaceRegister = new NullPersonPlaceRegister();

        private NullPersonPlaceRegister() : base(NullPlace.get(), NullPerson.get())
        {

        }

        public static PersonPlaceRegister get()
        {

            return personPlaceRegister;

        }

        public override PersonPlaceRegister setStatus(Status status)
        {

            return this;

        }

    }

}
