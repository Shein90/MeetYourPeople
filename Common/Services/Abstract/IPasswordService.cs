namespace Common.Services.Abstract;

public interface IPasswordService
{
    string GetHashFromPassword(string password);

    bool VerifyPassword(string hashedPassword, string password);
}