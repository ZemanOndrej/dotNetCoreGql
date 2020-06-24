to run this api you need to first run `yarn` command and then `yarn run merge && yarn run generate`
or equivalent npm commands
### there is a problem with grahpql code generator at the moment, it doesnt know how to handle c# keywords so there will be errors in `Types.cs` which you can fix by adding prefix `@` to the wrong property names
##TODO: find out how to generate better POCOs because graphql-code-generator doesnt generate interfaces 