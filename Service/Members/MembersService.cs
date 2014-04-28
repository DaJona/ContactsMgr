using DTO.System;
using Entity.Members;
using DAO.Members;
using System;
using Utilities;

namespace Service.Members
{
    public class MembersService
    {
        private MembersDAO membersDao;

        public MembersService()
        {
            membersDao = new MembersDAO();
        }

        public TransactionResult login(string memberUser, string memberPassword)
        {
            TransactionResult result = new TransactionResult();

            try
            {
                Member member;
                member = membersDao.getMember(memberUser, Encoding.sha512(memberUser, memberPassword));

                if (member != null)
                {
                    result.code = TransactionResult.transactionResultCode.Success;
                    result.object1 = member;
                }
                else
                {
                    result.code = TransactionResult.transactionResultCode.Failed;
                    result.failureReason = "ErrorUsuarioContrasenaIncorrectos";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public TransactionResult createMember(Member enMember)
        {
            TransactionResult result = new TransactionResult();

            try
            {
                // The member email must be unique
                Member existingMember = membersDao.getMember(enMember.email);
                if (existingMember != null)
                {
                    result.code = TransactionResult.transactionResultCode.Failed;
                    result.failureReason = "ErrorEmailYaExiste";
                    return result;
                }

                // Overwrite the member password with more secure one
                enMember.password = Encoding.sha512(enMember.email, enMember.password);

                membersDao.createMember(enMember);

                result.code = TransactionResult.transactionResultCode.Success;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
