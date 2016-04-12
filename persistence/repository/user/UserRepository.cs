using System;
using chat.model;

namespace persistence.repository.user
{
    public interface UserRepository
    {
        User verifyUser(String userName, String password);
    }
}
