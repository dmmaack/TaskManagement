using Bogus;
using Bogus.DataSets;
using TaskManagement.Core.Enums;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Tests.Fixture;

public class TaskEntityFixture
{
    public static TaskEntity CreateValid_TaskEntity() => 
        new TaskEntity(id: new Randomizer().Int(0, 1000),
            title: string.Join(" ", new Lorem().Words(num: 4)),
            description: new Lorem().Text(), 
            startDate: DateTime.Now.AddDays(1), 
            endDate: DateTime.Now.AddDays(2), 
            registerDate: DateTime.Now, 
            status: (int)StatusEnum.Pending, 
            priority: (int)PriorityTaskEnum.Middle,
            userId: 2, 
            assignedTo: 3
        );
                
        public static TaskEntity CreateInvalidTitle_TaskEntity() => 
            new TaskEntity(id: new Randomizer().Int(0,1000),
                title: string.Empty,
                description: new Lorem().Text(), 
                startDate: DateTime.Now.AddDays(1), 
                endDate: DateTime.Now.AddDays(2), 
                registerDate: DateTime.Now, 
                status: (int)StatusEnum.Pending, 
                priority: (int)PriorityTaskEnum.Middle,
                userId: 2, 
                assignedTo: 3
            );

        public static List<TaskEntity> CreateListValidTask(int limit = 5)
        {
            var list = new List<TaskEntity>();

            for (int i = 0; i < limit; i++)
                list.Add(CreateValid_TaskEntity());

            return list;
        }
}