﻿using OpenroomDapr.Shared.Model;

namespace OpenroomDapr.Server.Services;

public class PersonDataAccess : IPersonDataAccess
{
    //private readonly string _connectionString = "Host=hansken.db.elephantsql.com;Database=xrbmpoui;User Id=xrbmpoui;Password=i38x7v1O3aNteoNxteJNB5thtPfKqqxn;";
    private readonly string _connectionString;
    private readonly ILogger<PersonDataAccess> _logger;

    public PersonDataAccess(string connectionString, ILogger<PersonDataAccess> logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public async Task<IEnumerable<MyPerson>> GetPersons(CancellationToken cancellationToken)
    {
        using NpgsqlConnection connection = new(_connectionString);
        Stopwatch stopwatch = Stopwatch.StartNew();
        IEnumerable<MyPerson> persons = await connection.QueryAsync<MyPerson>(
            @"select
                person.*,
                createdby.alias as createdbyname, 
                modifiedby.alias as modifiedbyname
            from myperson person
            left join myperson createdby on createdby.id = person.createdby
            left join myperson modifiedby on modifiedby.id = person.modifiedby
            limit 200
            ;");
        stopwatch.Stop();
        _logger.LogDebug("{methodName} returned {result} in {stopwatch} ticks", nameof(GetPersons), JsonConvert.SerializeObject(persons), stopwatch.Elapsed);
        return persons;
    }

    public async Task<MyInteger> UpdateMyPerson(MyPerson person, CancellationToken cancellationToken)
    {
        string updateQuery = "update myperson set modifiedby = @modifiedby, modifieddate = @modifieddate where id = @id";
        using NpgsqlConnection connection = new(_connectionString);
        Stopwatch stopwatch = Stopwatch.StartNew();
        int response = await connection.ExecuteAsync(updateQuery, new
        {
            modifiedby = person.ModifiedBy,
            modifieddate = person.ModifiedDate,
            id = person.Id
        });
        stopwatch.Stop();
        _logger.LogDebug("{methodName} returned {response} for input of {input} in {stopwatch} ticks", nameof(UpdateMyPerson), response, JsonConvert.SerializeObject(person), stopwatch.Elapsed);
        MyInteger myInteger = new()
        {
            Value = response,
            Id = person.Id,
            ExternalId = person.ExternalId,
            CreatedBy = person.CreatedBy,
            CreatedDate = person.CreatedDate,
            ModifiedBy = person.ModifiedBy,
            ModifiedDate = person.ModifiedDate
        };
        return myInteger;
    }
}
