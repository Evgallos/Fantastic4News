using System;

namespace Fantastic4News.Services
{
    public interface INewsLetterService
    {
        bool CheckIfUserHasNewsLetter(string usrId);

        void NewsletterChange(string usrId);
    }
}
