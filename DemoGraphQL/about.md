# GraphQL

## About

Data quary and manipulation language for APIs.

2012 Fcebook, ;

## Query, Mutation.

REST, CRUD - create, read, update, delete.

GET: 'https://faculty/get-students'

```
{Name:"", Group: "", Rating: [], ...}
```

overfetching;


**Query**: read;

POST: 'https://faculty/graphql'

BODy:

```
{
  students {
	name
	group
  }
}
```


**Mutation**: create, update, delete.

Subscriptions: websockets;

