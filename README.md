# CUSTIS.NetCore.EF.MigrationGenerationExtensions.PostgreSQL

[![coverage report](https://git.custis.ru/pub/lightbox/badges/main/coverage.svg)](https://git.custis.ru/pub/lightbox/-/commits/main)

Lightweight implementation of the [Outbox pattern](https://microservices.io/patterns/data/transactional-outbox.html), which makes it available to send messages only when DB transaction is successfully committed.

Known limitations:
- doesn't drop deleted objects