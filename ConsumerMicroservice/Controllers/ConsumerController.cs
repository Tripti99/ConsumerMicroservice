using ConsumerMicroservice.Models;
using ConsumerMicroservice.ServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly IConsumerService _consumerService;

        public ConsumerController(IConsumerService consumerService)
        {

            _consumerService = consumerService;
        }
        [HttpGet("GetConsumer")]
        public IEnumerable<Consumer> GetConsumer()
        {
            return _consumerService.GetConsumers();
        }
        [HttpGet("GetBusinessMaster")]
        public IEnumerable<BusinessMaster> GetBusienssMaster()
        {
            return _consumerService.GetBusinessMaster();
        }

        [HttpGet("GetPropertyMaster")]
        public IEnumerable<PropertyMaster> GetPropertyMaster()
        {
            return _consumerService.GetPropertyMaster();
        }
        [HttpGet("GetBusiness")]
        public IEnumerable<Business> GetBusiness()
        {
            return _consumerService.GetBusiness();
        }

        // Display of all Property
        [HttpGet("GetProperty")]
        public IEnumerable<Property> GetProperty()
        {
            return _consumerService.GetProperty();
        }

        // Display of consumer by ID
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetConsumerById")]
        public ActionResult GetConsumerById(int ConsumerId)
        {

            var obj = _consumerService.GetConsumer(ConsumerId);
            if (obj == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Consumer Not Found"
                });
            }
            else
            {
                return Ok(new
                {
                    Status = 200,
                   ConsumerDetails = obj
                });
            }
        }
        //    return Ok(obj);
        //}
        // Display of business by ID
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetBusinessById")]
        public ActionResult GetBusinessById(int BusinessId)
        {
            var obj = _consumerService.GetBusinesss(BusinessId);
            if (obj == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Business Not Found"
                });
            }
            else
            {
                return Ok(new
                {
                    Status = 200,
                   BusinessDetails = obj
                });
            }
        }
        //    return Ok(obj);
        //}

        // Display of property by ID
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetPropertyById")]
        public ActionResult GetPropertyById(int PropertyId)
        {
            var obj = _consumerService.GetProperties(PropertyId);
            if (obj == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Property Not Found"
                });
            }
            else
            {
                return Ok(new
                {
                    Status = 200,
                    PropertyDetails = obj
                });
            }
        }
        //    return Ok(obj);
        //}
        // Create Consumer using HTTPPOST
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("CreateConsumer")]
        //[EnableCors("AllowAllOrigins")]
        [ProducesResponseType(201, Type = typeof(Consumer))]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreateConsumer([FromBody] Consumer consumer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }

                if (!_consumerService.CreateConsumer(consumer) && !_consumerService.ConsumerExists(consumer.ConsumerId))
                {
                    return new ObjectResult("Database insertion Error") { StatusCode = 500 };
                }
                return new CreatedResult("GetConsumer", new { id = consumer.ConsumerId });
            }
            catch (Exception e)
            {
            
                return new ObjectResult("DataBase Error, Check for Id") { StatusCode = 500 };
            }
        }
        // Create Business using HTTPPOST
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("CreateBusiness")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreateBusiness([FromBody] Business? business)
        {
            try
            {
               
                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }

                if (_consumerService.CreateBusiness(business))
                {
                    return new CreatedResult("GetBusiness", new { id = business.BusinessId });
                }
                return new ObjectResult("Database Error") { StatusCode = 500 };
            }
            catch (Exception e)
            {
               
                return new ObjectResult("Database Error, Check for Id" + e.Message) { StatusCode = 500 };
            }
        }
        // Create Property using HTTPPOST
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("CreateProperty")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreateProperty([FromBody] Property? property)
        {
            try
            {
               
                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }

                if (_consumerService.CreateProperty(property))
                {
                    return new CreatedResult("GetProperty", new { id = property.PropertyId });
                }
                return new ObjectResult("Database Error") { StatusCode = 500 };
            }
            catch (Exception e)
            {
                
                return new ObjectResult("Database Error, Check for Id" + e.Message) { StatusCode = 500 };
            }
        }

        // udpate consumer
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("UpdateConsumer")]
        public ActionResult UpdateConsumer(int ConsumerId, [FromBody] Consumer consumer)
        {
            try
            {
               
                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }

                var updateResult = _consumerService.UpdateConsumer(ConsumerId, consumer);

                if (updateResult)
                {
                    return new CreatedResult("GetConsumerById", new { id = consumer.ConsumerId });
                }
                return new ObjectResult("Database insertion Error") { StatusCode = 500 };
            }
            catch (Exception e)
            {
               
                return new ObjectResult("DataBase Error, Check for Id") { StatusCode = 500 };
            }
        }

        // udpate business
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("UpdateBusiness")]
        public ActionResult UpdateBusiness(int BusinessId, [FromBody] Business business)
        {
            try
            {
               
                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }

                var updateResult = _consumerService.UpdateBusiness(BusinessId, business);

                if (updateResult)
                {
                    return new CreatedResult("GetBusinessById", new { id = business.BusinessId });
                }
                return new ObjectResult("Database insertion Error") { StatusCode = 500 };
            }
            catch (Exception e)
            {
               
                return new ObjectResult("DataBase Error, Check for Id") { StatusCode = 500 };
            }
        }

        // udpate property  
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("UpdateProperty")]
        public ActionResult UpdateProperty(int PropertyId, [FromBody] Property property)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }

                var updateResult = _consumerService.UpdateProperty(PropertyId, property);

                if (updateResult)
                {
                    return new CreatedResult("GetPropertyById", new { id = property.PropertyId });
                }
                return new ObjectResult("Database insertion Error") { StatusCode = 500 };
            }
            catch (Exception e)
            {
               
                return new ObjectResult("DataBase Error, Check for Id") { StatusCode = 500 };
            }
        }

        // Delete Consumer by passing Id
        [HttpDelete("DeleteConsumer")]
        public ActionResult DeleteConsumer(int ConsumerId)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }

                var deleteResult = _consumerService.DeleteConsumer(ConsumerId);

                if (deleteResult)
                {
                    return new CreatedResult("GetConsumerById", new { id = ConsumerId });
                }
                return new ObjectResult("Database deletion Error") { StatusCode = 500 };
            }
            catch (Exception e)
            {
              
                return new ObjectResult("DataBase Error, Check for Id") { StatusCode = 500 };
            }
        }

        // Delete busienss by passing Id
        [HttpDelete("DeleteBusiness")]
        public ActionResult DeleteBusiness(int BusinessId)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }

                var deleteResult = _consumerService.DeleteBusiness(BusinessId);

                if (deleteResult)
                {
                    return new CreatedResult("GetBusinessById", new { id = BusinessId });
                }
                return new ObjectResult("Database deletion Error") { StatusCode = 500 };
            }
            catch (Exception e)
            {
               
                return new ObjectResult("DataBase Error, Check for Id") { StatusCode = 500 };
            }
        }

        // Delete Property by passing Id
        [HttpDelete("DeleteProperty")]
        public ActionResult DeleteProperty(int PropertyId)
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(ModelState);
                }

                var deleteResult = _consumerService.DeleteProperty(PropertyId);

                if (deleteResult)
                {
                    return new CreatedResult("GetPropertyById", new { id = PropertyId });
                }
                return new ObjectResult("Database deletion Error") { StatusCode = 500 };
            }
            catch (Exception e)
            {
               
                return new ObjectResult("DataBase Error, Check for Id") { StatusCode = 500 };
            }
        }
    }
}

  