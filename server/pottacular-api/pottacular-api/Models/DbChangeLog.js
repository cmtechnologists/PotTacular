// Pottacular db changelog v1.0.0
// create db by running script via mongo command:
// c:\> mongo < "/the/path/to/yourFile.js"
// or in the Mongo shell: 
// >load("DbChangeLog.js")
var db = connect("mongodb://localhost:27017/PottacularDb"),
    initialData = null;
db.createCollection('TestRequests');
db.TestRequests.insertMany([
    {
        'TestRequestName': 'test insert 1'
    },
    {
        'TestRequestName': 'test insert 2'
    }
]);
