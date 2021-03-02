using GuessWho.Infra.TableStorage.Contracts;
using Microsoft.Azure.Cosmos.Table;
using System;

namespace GuessWho.Infra.TableStorage
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Matrix.PaymentGateway.Infra.TableStorage.Contracts.IAuditSigner" />
    public class AuditSigner : IAuditSigner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Matrix.PaymentGateway.Infra.TableStorage.Contracts.IAuditSigner" /> interface.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isInsert">if set to <c>true</c> [is insert].</param>
        public void Sign(TableOperation operation)
        {
            if(!(operation.Entity is IAudit))
            {
                return;
            }

            var entity = operation.Entity as IAudit;
            entity.LastChangeDate = DateTime.UtcNow;

            if (operation.OperationType == TableOperationType.Insert)
            {
                entity.CreationDate = DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Signs the specified operation batch.
        /// </summary>
        /// <param name="operationBatch">The operation batch.</param>
        public void Sign(TableBatchOperation operationBatch)
        {
            foreach(var operation in operationBatch)
            {
                Sign(operation);
            }
        }
    }
}
