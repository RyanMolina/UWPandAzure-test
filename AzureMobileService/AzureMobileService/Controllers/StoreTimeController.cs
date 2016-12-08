using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using AzureMobileService.DataObjects;
using AzureMobileService.Models;

namespace AzureMobileService.Controllers
{
    public class StoreTimeController : TableController<StoreTimeDTO>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<StoreTimeDTO>(context, Request, Services);
        }

        // GET tables/StoreTime
        public IQueryable<StoreTimeDTO> GetAllStoreTimeDTO()
        {
            return Query(); 
        }

        // GET tables/StoreTime/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<StoreTimeDTO> GetStoreTimeDTO(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/StoreTime/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<StoreTimeDTO> PatchStoreTimeDTO(string id, Delta<StoreTimeDTO> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/StoreTime
        public async Task<IHttpActionResult> PostStoreTimeDTO(StoreTimeDTO item)
        {
            StoreTimeDTO current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/StoreTime/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteStoreTimeDTO(string id)
        {
             return DeleteAsync(id);
        }

    }
}