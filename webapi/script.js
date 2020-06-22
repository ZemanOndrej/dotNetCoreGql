const {loadFilesSync} = require('@graphql-tools/load-files');
const {mergeTypeDefs} = require('@graphql-tools/merge');
const {print} = require('graphql');
const fs = require('fs');
const path = `${__dirname}/graphql/**/*.graphql`;
const loadedFiles = loadFilesSync(path);
console.log(loadedFiles, path)
const typeDefs = mergeTypeDefs(loadedFiles, {all: true});
const printedTypeDefs = print(typeDefs);
fs.writeFileSync('./generated/joined.graphql', printedTypeDefs);
