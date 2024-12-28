# DotnetCacheStrategies

## Cache Aside Project

The cache-aside pattern (also known as "lazy loading") is a widely used caching strategy where the application is responsible for fetching data and managing the cache explicitly. This pattern ensures that only the required data is cached, and the cache remains consistent with the underlying data store.

```
Step 1: Try to Get Data from the Cache
The application first attempts to retrieve the data from the cache.
If the data is found in the cache, it's a cache hit, and the application proceeds to use the cached data.
If the data is not found in the cache, it's a cache miss, and the application moves to the next step.

Step 2: Load Data from the Data Store
In the case of a cache miss, the application retrieves the data from the underlying data store (e.g., a database or API).

Step 3: Store Data in the Cache
After retrieving the data from the data store, the application writes the data into the cache for future use.
Typically, the cache entry is set with a time-to-live (TTL) value to ensure the data eventually expires and can be refreshed.

Step 4: Return Data to the Caller
The application returns the requested data to the caller, whether it was retrieved from the cache or the data store.

Step 5: Handle Cache Expiry or Invalidations
When a cache entry expires or is invalidated, the next request for that data will result in a cache miss.
The process of loading the data from the data store and re-caching it repeats.
```

Repository Pattern and Dependency Injection Pattern are used in the project.

## Write-through Project

This strategy ensures immediate consistency between the cache and the database because both are updated during every write operation.

```
Step 1 : Cache Request (Read/Write):
Read Request: The application checks the cache first for the required data.
Write Request: The application writes data to the cache (temporary storage) and immediately writes the same data to the database (persistent storage).

Step 2 : Write Hit (Data in Cache):
If the data is already in the cache, the cache is updated, and the same data is immediately written to the database to keep both consistent.

Step 3 : Write Miss (Data NOT in Cache):
If the data is not in the cache, it is retrieved from the database, then stored in the cache for future reads.
The new data is then written to both the cache and database.
```

