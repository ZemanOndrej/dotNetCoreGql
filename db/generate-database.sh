#!/usr/bin/env bash
source ./.env
echo "Running dotnet scaffold: "$CONNECTION_STRING
dotnet ef dbcontext scaffold $CONNECTION_STRING Npgsql.EntityFrameworkCore.PostgreSQL -o Models --force -c YogaDbContext