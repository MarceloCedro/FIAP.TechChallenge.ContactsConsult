﻿using FIAP.TechChallenge.ContactsConsult.Application.Applications;
using FIAP.TechChallenge.ContactsConsult.Domain.Entities;
using FIAP.TechChallenge.ContactsConsult.Domain.Interfaces.Applications;
using FIAP.TechChallenge.ContactsConsult.Domain.Interfaces.ElasticSearch;
using FIAP.TechChallenge.ContactsConsult.Domain.Interfaces.Repositories;
using FIAP.TechChallenge.ContactsConsult.Domain.Interfaces.Services;
using FIAP.TechChallenge.ContactsConsult.Domain.Services;
using FIAP.TechChallenge.ContactsConsult.Infrastructure.Repositories;
using FIAP.TechChallenge.ContactsConsult.IntegrationTest.Config;
using Microsoft.Extensions.Logging;
using Moq;

namespace FIAP.TechChallenge.ContactsConsult.IntegrationTest.Validations
{
    public class ContactApplicationTests : BaseServiceTests
    {
        private readonly IContactService _contactService;
        private readonly IContactApplication _contactApplicationException;
        private readonly IContactApplication _contactApplication;
        private readonly IContactRepository _contactRepository;
        private readonly Mock<IElasticClient<Contact>> _elasticClientMock;
        private Mock<ILogger<ContactService>> _loggerServiceMock;
        private Mock<ILogger<ContactApplication>> _loggerApplicationMock;
        public readonly Random RandomId;

        public ContactApplicationTests()
        {
            _elasticClientMock = new Mock<IElasticClient<Contact>>();
            _contactRepository = new ContactRepository(_context);
            _loggerServiceMock = new Mock<ILogger<ContactService>>();
            _loggerApplicationMock = new Mock<ILogger<ContactApplication>>();
            _contactService = new ContactService(_contactRepository, _loggerServiceMock.Object, _elasticClientMock.Object);
            _contactApplication = new ContactApplication(_contactService, _loggerApplicationMock.Object);
            _contactApplicationException = new ContactApplication(null, _loggerApplicationMock.Object);
            RandomId = new Random();
        }

        [Fact]
        public async Task GetAllContactSuccessAsync()
        {
            var contact1 = ContactFixtures.CreateFakeContact(RandomId.Next(999999999));
            var contact2 = ContactFixtures.CreateFakeContact(RandomId.Next(999999999));
            var contact3 = ContactFixtures.CreateFakeContact(RandomId.Next(999999999));

            await _context.AddRangeAsync(contact1, contact2, contact3);

            await SaveChanges();

            var contactList = await _contactApplication.GetAllContactsAsync();
            Assert.NotNull(contactList);
            Assert.NotEmpty(contactList);
        }

        [Fact]
        public async Task GetAllContactExceptionAsync()
        {
            var contact1 = ContactFixtures.CreateFakeContact(RandomId.Next(999999999));
            var contact2 = ContactFixtures.CreateFakeContact(RandomId.Next(999999999));
            var contact3 = ContactFixtures.CreateFakeContact(RandomId.Next(999999999));

            await _context.AddRangeAsync(contact1, contact2, contact3);

            await SaveChanges();

            var contactList = await _contactApplicationException.GetAllContactsAsync();
            Assert.Null(contactList);
        }

        [Fact]
        public async Task GetContactByIdExceptionAsync()
        {
            var contact1 = ContactFixtures.CreateFakeContact(RandomId.Next(999999999));
            var contact2 = ContactFixtures.CreateFakeContact(RandomId.Next(999999999));
            var contact3 = ContactFixtures.CreateFakeContact(RandomId.Next(999999999));

            await _context.AddRangeAsync(contact1, contact2, contact3);

            await SaveChanges();

            var contactList = await _contactApplicationException.GetContactByIdAsync(contact2.Id);
            Assert.Null(contactList);
        }

        [Fact]
        public async Task GetContactByIdSuccessAsync()
        {
            var contact1 = ContactFixtures.CreateFakeContact(RandomId.Next(999999999));
            var contact2 = ContactFixtures.CreateFakeContact(RandomId.Next(999999999));
            var contact3 = ContactFixtures.CreateFakeContact(RandomId.Next(999999999));

            await _context.AddRangeAsync(contact1, contact2, contact3);

            await SaveChanges();

            var contactList = await _contactApplication.GetContactByIdAsync(contact2.Id);
            Assert.NotNull(contactList);
            Assert.Equal(contactList.Id, contact2.Id);
        }

        [Fact]
        public async Task GetContactByAreaCodeExceptionAsync()
        {
            var contact1 = ContactFixtures.CreateFakeContact(RandomId.Next(999999999));
            var contact2 = ContactFixtures.CreateFakeContact(RandomId.Next(999999999));
            var contact3 = ContactFixtures.CreateFakeContact(RandomId.Next(999999999));

            await _context.AddRangeAsync(contact1, contact2, contact3);

            await SaveChanges();

            var contactList = await _contactApplicationException.GetContactsByAreaCodeAsync(contact2.AreaCode);
            Assert.Null(contactList);
        }

        [Fact]
        public async Task GetContactByAreaCodeSuccessAsync()
        {
            var contact1 = ContactFixtures.CreateFakeContact(RandomId.Next(999999999));
            var contact2 = ContactFixtures.CreateFakeContact(RandomId.Next(999999999));
            var contact3 = ContactFixtures.CreateFakeContact(RandomId.Next(999999999));

            await _context.AddRangeAsync(contact1, contact2, contact3);

            await SaveChanges();

            var contactList = await _contactApplication.GetContactsByAreaCodeAsync(contact2.AreaCode);
            Assert.NotNull(contactList);
            Assert.NotEmpty(contactList);
        }
    }
}
