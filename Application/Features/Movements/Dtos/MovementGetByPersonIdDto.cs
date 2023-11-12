using Application.Features.Persons.Dtos;

namespace Application.Features.Movements.Dtos
{
    public class MovementGetByPersonIdDto
    {
        public int Id { get; set; }
        public string EventType { get; set; }
        public DateTime MovementTime { get; set; }
        public PersonDto Person { get; set; }
    }
}
