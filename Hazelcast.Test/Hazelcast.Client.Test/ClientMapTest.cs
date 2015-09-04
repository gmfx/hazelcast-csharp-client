using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Hazelcast.Client.Model;
using Hazelcast.Core;
using NUnit.Framework;

namespace Hazelcast.Client.Test
{
    [TestFixture]
    public class ClientMapTest : HazelcastBaseTest
    {
        //
        [SetUp]
        public void Init()
        {
            map = Client.GetMap<object, object>(Name);
        }

        [TearDown]
        public static void Destroy()
        {
            map.Clear();
        }

        //internal const string name = "test";

        internal static IMap<object, object> map;
        ///// <exception cref="System.Exception"></exception>
        //[Test]
        //public void testPredicateListenerWithPortableKey() throws InterruptedException {
        //    //var tradeMap = client.getMap("tradeMap");
        //    //final CountdownEvent CountdownEvent = new CountdownEvent(1);
        //    //final AtomicInteger atomicInteger = new AtomicInteger(0);

        //    var countdownEvent = new CountdownEvent(1);
        //    EntryAdapter<object,object> listener=new EntryAdapter<object, object>(
        //        delegate(EntryEvent<object, object> @event) { countdownEvent.Signal(); },
        //        delegate(EntryEvent<object, object> @event) {  }, 
        //        delegate(EntryEvent<object, object> @event) {  },
        //        delegate(EntryEvent<object, object> @event) {  }  ); 

        //    //EntryListener listener = new EntryListener() {
        //    //    @Override
        //    //    public void entryAdded(EntryEvent event) {
        //    //        atomicInteger.incrementAndGet();
        //    //        countdownEvent.Signal();
        //    //    }

        //    //    @Override
        //    //    public void entryRemoved(EntryEvent event) {
        //    //    }

        //    //    @Override
        //    //    public void entryUpdated(EntryEvent event) {
        //    //    }

        //    //    @Override
        //    //    public void entryEvicted(EntryEvent event) {
        //    //    }
        //    //};
        //    var key = new AuthenticationRequest(new UsernamePasswordCredentials("a", "b"));
        //    tradeMap.addEntryListener(listener, key, true);

        //    var key2 = new AuthenticationRequest(new UsernamePasswordCredentials("a", "c"));
        //    tradeMap.put(key2, 1);

        //    Assert.assertFalse(CountdownEvent.await(15, TimeUnit.SECONDS));
        //    Assert.assertEquals(0,atomicInteger.get());
        //}

        //    @Test
        //    public void testBasicPredicate() {
        //        fillMap();
        //        final Collection collection = map.values(new SqlPredicate("this == value1"));
        //        Assert.assertEquals("value1", collection.iterator().next());
        //        final Set set = map.keySet(new SqlPredicate("this == value1"));
        //        Assert.assertEquals("key1", set.iterator().next());
        //        final Set<Map.Entry<String, String>> set1 = map.entrySet(new SqlPredicate("this == value1"));
        //        Assert.assertEquals("key1", set1.iterator().next().getKey());
        //        Assert.assertEquals("value1", set1.iterator().next().getValue());
        //    }
        private void FillMap()
        {
            for (var i = 0; i < 10; i++)
            {
                map.Put("key" + i, "value" + i);
            }
        }

        ///// <summary>Issue #923</summary>
        //[Test]
        //public virtual void TestPartitionAwareKey()
        //{
        //    string name = "testPartitionAwareKey";
        //    PartitionAwareKey key = new PartitionAwareKey("key", "123");
        //    string value = "value";
        //    IMap<object, object> map1 = server.GetMap(name);
        //    map1.Put(key, value);
        //    Assert.AreEqual(value, map1.Get(key));
        //    IMap<object, object> map2 = client.GetMap(name);
        //    Assert.AreEqual(value, map2.Get(key));
        //}

        //[System.Serializable]
        //private class PartitionAwareKey : IPartitionAware
        //{
        //    private readonly string key;

        //    private readonly string pk;

        //    public PartitionAwareKey(string key, string pk)
        //    {
        //        this.key = key;
        //        this.pk = pk;
        //    }

        //    public virtual object GetPartitionKey()
        //    {
        //        return pk;
        //    }
        //}

        ///// <summary>Issue #996</summary>
        ///// <exception cref="System.Exception"></exception>
        //[Test]
        //public virtual void TestEntryListener()
        //{
        //    CountdownEvent gateAdd = new CountdownEvent(2);
        //    CountdownEvent gateRemove = new CountdownEvent(1);
        //    CountdownEvent gateEvict = new CountdownEvent(1);
        //    CountdownEvent gateUpdate = new CountdownEvent(1);
        //    string mapName = "testEntryListener";
        //    IMap<object, object> serverMap = server.GetMap(mapName);
        //    serverMap.Put(3, new ClientMapTest.Deal(3));
        //    IMap<object, object> clientMap = client.GetMap(mapName);
        //    Assert.AreEqual(1, clientMap.Size());
        //    EntryListener listener = new ClientMapTest.EntListener(gateAdd, gateRemove, gateEvict, gateUpdate);
        //    //        clientMap.addEntryListener(listener, new SqlPredicate("id=1"), 2, true);
        //    clientMap.Put(2, new ClientMapTest.Deal(1));
        //    clientMap.Put(2, new ClientMapTest.Deal(1));
        //    Sharpen.Collections.Remove(clientMap, 2);
        //    clientMap.Put(2, new ClientMapTest.Deal(1));
        //    clientMap.Evict(2);
        //    Assert.IsTrue(gateAdd.Await(10, TimeUnit.Seconds));
        //    Assert.IsTrue(gateRemove.Await(10, TimeUnit.Seconds));
        //    Assert.IsTrue(gateEvict.Await(10, TimeUnit.Seconds));
        //    Assert.IsTrue(gateUpdate.Await(10, TimeUnit.Seconds));
        //}

        //[System.Serializable]
        //internal class EntListener : EntryListener<int, ClientMapTest.Deal>
        //{
        //    private readonly CountdownEvent _gateAdd;

        //    private readonly CountdownEvent _gateRemove;

        //    private readonly CountdownEvent _gateEvict;

        //    private readonly CountdownEvent _gateUpdate;

        //    internal EntListener(CountdownEvent gateAdd, CountdownEvent gateRemove, CountdownEvent gateEvict, CountdownEvent gateUpdate)
        //    {
        //        _gateAdd = gateAdd;
        //        _gateRemove = gateRemove;
        //        _gateEvict = gateEvict;
        //        _gateUpdate = gateUpdate;
        //    }

        //    public virtual void EntryAdded(EntryEvent<int, ClientMapTest.Deal> arg0)
        //    {
        //        _gateAdd.Signal();
        //    }

        //    public virtual void EntryEvicted(EntryEvent<int, ClientMapTest.Deal> arg0)
        //    {
        //        _gateEvict.Signal();
        //    }

        //    public virtual void EntryRemoved(EntryEvent<int, ClientMapTest.Deal> arg0)
        //    {
        //        _gateRemove.Signal();
        //    }

        //    public virtual void EntryUpdated(EntryEvent<int, ClientMapTest.Deal> arg0)
        //    {
        //        _gateUpdate.Signal();
        //    }
        //}

        [Serializable]
        internal class Deal
        {
            internal int id;

            internal Deal(int id)
            {
                this.id = id;
            }

            public virtual int GetId()
            {
                return id;
            }

            public virtual void SetId(int id)
            {
                this.id = id;
            }
        }

        [Test]
        public virtual void TestAddIndex()
        {
            map.AddIndex("name", true);
        }

        /// <exception cref="System.Exception"></exception>
        [Test]
        public virtual void TestAsyncGet()
        {
            FillMap();
            var f = map.GetAsync("key1");

            var o = f.Result;
            Assert.AreEqual("value1", o);
        }

        /// <exception cref="System.Exception"></exception>
        [Test]
        public virtual void TestAsyncPut()
        {
            FillMap();
            var f = map.PutAsync("key3", "value");

            Assert.False(f.IsCompleted);

            var o = f.Result;
            Assert.AreEqual("value3", o);
            Assert.AreEqual("value", map.Get("key3"));
        }

        /// <exception cref="System.Exception"></exception>
        [Test]
        public virtual void TestAsyncPutWithTtl()
        {
            var latch = new CountdownEvent(1);

            map.AddEntryListener(new EntryAdapter<object, object>(
                delegate { },
                delegate { },
                delegate { },
                delegate { latch.Signal(); }
                ), true);

            var f1 = map.PutAsync("key", "value1", 3, TimeUnit.SECONDS);
            Assert.IsNull(f1.Result);
            Assert.AreEqual("value1", map.Get("key"));

            Assert.IsTrue(latch.Wait(TimeSpan.FromSeconds(10)));
            Assert.IsNull(map.Get("key"));
        }

        /// <exception cref="System.Exception"></exception>
        [Test]
        public virtual void TestAsyncRemove()
        {
            FillMap();
            var f = map.RemoveAsync("key4");
            Assert.False(f.IsCompleted);

            var o = f.Result;
            Assert.AreEqual("value4", o);
            Assert.AreEqual(9, map.Size());
        }

        /// <exception cref="System.Exception"></exception>
        [Test]
        public virtual void TestContains()
        {
            FillMap();
            Assert.IsFalse(map.ContainsKey("key10"));
            Assert.IsTrue(map.ContainsKey("key1"));
            Assert.IsFalse(map.ContainsValue("value10"));
            Assert.IsTrue(map.ContainsValue("value1"));
        }

        [Test]
        public virtual void TestEntrySet()
        {
            map.Put("key1", "value1");
            map.Put("key2", "value2");
            map.Put("key3", "value3");

            var keyValuePairs = map.EntrySet();

            IDictionary<object, object> tempDict = new Dictionary<object, object>();
            foreach (var keyValuePair in keyValuePairs)
            {
                tempDict.Add(keyValuePair);
            }

            Assert.True(tempDict.ContainsKey("key1"));
            Assert.True(tempDict.ContainsKey("key2"));
            Assert.True(tempDict.ContainsKey("key3"));

            object value;
            tempDict.TryGetValue("key1", out value);
            Assert.AreEqual("value1", value);

            tempDict.TryGetValue("key2", out value);
            Assert.AreEqual("value2", value);

            tempDict.TryGetValue("key3", out value);
            Assert.AreEqual("value3", value);
        }

        [Test]
        public virtual void TestEntrySetPredicate()
        {
            map.Put("key1", "value1");
            map.Put("key2", "value2");
            map.Put("key3", "value3");

            var keyValuePairs = map.EntrySet(new SqlPredicate("this == value1"));
            Assert.AreEqual(1, keyValuePairs.Count);

            var enumerator = keyValuePairs.GetEnumerator();
            enumerator.MoveNext();
            Assert.AreEqual("key1", enumerator.Current.Key);
            Assert.AreEqual("value1", enumerator.Current.Value);
        }

        [Test]
        public virtual void TestEntryView()
        {
            var item = ItemGenerator.GenerateItem(1);
            map.Put("key1", item);
            map.Get("key1");
            map.Get("key1");


            var entryview = map.GetEntryView("key1");
            var value = entryview.GetValue() as Item;

            Assert.AreEqual("key1", entryview.GetKey());
            Assert.True(item.Equals(value));
            //Assert.AreEqual(2, entryview.GetHits());
            //Assert.True(entryview.GetCreationTime() > 0);
            //Assert.True(entryview.GetLastAccessTime() > 0);
            //Assert.True(entryview.GetLastUpdateTime() > 0);
        }

        [Test]
        public virtual void TestEvict()
        {
            map.Put("key1", "value1");
            Assert.AreEqual("value1", map.Get("key1"));

            map.Evict("key1");

            Assert.AreEqual(0, map.Size());
            Assert.AreNotEqual("value1", map.Get("key1"));
        }

        [Test]
        public virtual void TestFlush()
        {
            map.Flush();
        }

        /// <exception cref="System.Exception"></exception>
        [Test]
        public virtual void TestForceUnlock()
        {
            map.Lock("key1");
            var latch = new CountdownEvent(1);

            var t1 = new Thread(delegate(object o)
            {
                map.ForceUnlock("key1");
                latch.Signal();
            });

            t1.Start();

            Assert.IsTrue(latch.Wait(TimeSpan.FromSeconds(100)));
            Assert.IsFalse(map.IsLocked("key1"));
        }

        [Test]
        public virtual void TestGet()
        {
            FillMap();
            for (var i = 0; i < 10; i++)
            {
                var o = map.Get("key" + i);
                Assert.AreEqual("value" + i, o);
            }
        }

        [Test]
        public virtual void TestGetAllExtreme()
        {
            IDictionary<object, object> mm = new Dictionary<object, object>();
            const int keycount = 1000;

            //insert dummy keys and values 
            foreach (var itemIndex in Enumerable.Range(0, keycount))
            {
                mm.Add(itemIndex.ToString(), itemIndex.ToString());
            }

            map.PutAll(mm);
            Assert.AreEqual(map.Size(), keycount);

            var dictionary = map.GetAll(mm.Keys);
            Assert.AreEqual(dictionary.Count, keycount);
        }

        //TODO map store
        [Test]
        public virtual void TestGetAllPutAll()
        {
            IDictionary<object, object> mm = new Dictionary<object, object>();
            for (var i = 0; i < 100; i++)
            {
                mm.Add(i, i);
            }
            map.PutAll(mm);
            Assert.AreEqual(map.Size(), 100);
            for (var i_1 = 0; i_1 < 100; i_1++)
            {
                Assert.AreEqual(map.Get(i_1), i_1);
            }
            var ss = new HashSet<object> {1, 3};

            var m2 = map.GetAll(ss);
            Assert.AreEqual(m2.Count, 2);

            object gv;
            m2.TryGetValue(1, out gv);
            Assert.AreEqual(gv, 1);

            m2.TryGetValue(3, out gv);
            Assert.AreEqual(gv, 3);
        }

        [Test]
        public virtual void TestGetEntryView()
        {
            map.Put("item0", "value0");
            map.Put("item1", "value1");
            map.Put("item2", "value2");

            var entryView = map.GetEntryView("item1");

            Assert.AreEqual(0, entryView.GetHits());

            Assert.AreEqual("item1", entryView.GetKey());
            Assert.AreEqual("value1", entryView.GetValue());
        }

        /// <exception cref="System.Exception"></exception>
        [Test]
        public virtual void TestIsEmpty()
        {
            Assert.IsTrue(map.IsEmpty());
            map.Put("key1", "value1");
            Assert.IsFalse(map.IsEmpty());
        }

        [Test]
        public virtual void TestKeySet()
        {
            map.Put("key1", "value1");

            var keySet = map.KeySet();

            var enumerator = keySet.GetEnumerator();

            enumerator.MoveNext();
            Assert.AreEqual("key1", enumerator.Current);
        }

        [Test]
        public void TestKeySetPredicate()
        {
            FillMap();

            var values = map.KeySet(new SqlPredicate("this == value1"));
            Assert.AreEqual(1, values.Count);
            var enumerator = values.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("key1", enumerator.Current);
        }

        //    @Test
        //    public void testSubmitToKey() throws Exception {
        //        map.put(1,1);
        //        Future f = map.submitToKey(1, new IncrementorEntryProcessor());
        //        Assert.assertEquals(2,f.get());
        //        Assert.assertEquals(2,map.get(1));
        //    }
        //    @Test
        //    public void testSubmitToNonExistentKey() throws Exception {
        //        Future f = map.submitToKey(11, new IncrementorEntryProcessor());
        //        Assert.assertEquals(1,f.get());
        //        Assert.assertEquals(1,map.get(11));
        //    }
        //    @Test
        //    public void testSubmitToKeyWithCallback() throws  Exception
        //    {
        //        map.put(1,1);
        //        final CountdownEvent latch = new CountdownEvent(1);
        //        ExecutionCallback executionCallback = new ExecutionCallback() {
        //            @Override
        //            public void onResponse(Object response) {
        //                latch.Signal();
        //            }
        //
        //            @Override
        //            public void onFailure(Throwable t) {
        //            }
        //        };
        //
        //        map.submitToKey(1,new IncrementorEntryProcessor(),executionCallback);
        //        Assert.assertTrue(latch.await(5, TimeUnit.SECONDS));
        //        Assert.assertEquals(2,map.get(1));
        //    }

        [Test]
        public void TestListener()
        {
            var latch1Add = new CountdownEvent(5);
            var latch1Remove = new CountdownEvent(2);
            var latch2Add = new CountdownEvent(1);
            var latch2Remove = new CountdownEvent(1);
            var listener1 = new EntryAdapter<object, object>(
                delegate { latch1Add.Signal(); },
                delegate { latch1Remove.Signal(); },
                delegate { },
                delegate { });

            var listener2 = new EntryAdapter<object, object>(
                delegate { latch2Add.Signal(); },
                delegate { latch2Remove.Signal(); },
                delegate { },
                delegate { });

            var reg1 = map.AddEntryListener(listener1, false);
            var reg2 = map.AddEntryListener(listener2, "key3", true);

            Thread.Sleep(1000);

            map.Put("key1", "value1");
            map.Put("key2", "value2");
            map.Put("key3", "value3");
            map.Put("key4", "value4");
            map.Put("key5", "value5");

            Thread.Sleep(10000);

            map.Remove("key1");
            map.Remove("key3");

            Assert.IsTrue(latch1Add.Wait(TimeSpan.FromSeconds(1000)));
            Assert.IsTrue(latch1Remove.Wait(TimeSpan.FromSeconds(10)));
            Assert.IsTrue(latch2Add.Wait(TimeSpan.FromSeconds(5)));
            Assert.IsTrue(latch2Remove.Wait(TimeSpan.FromSeconds(5)));

            Assert.IsTrue(map.RemoveEntryListener(reg1));
            Assert.IsTrue(map.RemoveEntryListener(reg2));
        }

        [Test]
        public void TestListenerClearAll()
        {
            var latchClearAll = new CountdownEvent(1);

            var listener1 = new EntryAdapter<object, object>(
                delegate { },
                delegate { },
                delegate { },
                delegate { },
                delegate { },
                delegate { latchClearAll.Signal(); });

            var reg1 = map.AddEntryListener(listener1, false);

            map.Put("key1", "value1");
            //map.Put("key2", "value2");
            //map.Put("key3", "value3");
            //map.Put("key4", "value4");
            //map.Put("key5", "value5");

            map.Clear();

            Assert.IsTrue(latchClearAll.Wait(TimeSpan.FromSeconds(15)));
        }

        [Test]
        public void TestListenerEventOrder()
        {
            const int maxSize = 10000;
            var map2 = Client.GetMap<int, int>(Name);
            map2.Put(1, 0);

            var eventDataReceived = new Queue<int>();

            var listener = new EntryAdapter<int, int>(
                e => { },
                e => { },
                e => eventDataReceived.Enqueue(e.GetValue()),
                e => { });

            map2.AddEntryListener(listener, true);

            for (var i = 1; i < maxSize; i++)
            {
                map2.Put(1, i);
            }
            while (eventDataReceived.Count != maxSize - 1)
            {
                Thread.Sleep(100);
            }
            Assert.AreEqual(maxSize - 1, eventDataReceived.Count);

            var oldEventData = -1;
            foreach (var eventData in eventDataReceived)
            {
                Assert.Less(oldEventData, eventData);
                oldEventData = eventData;
            }
        }

        [Test]
        public void TestListenerExtreme()
        {
            const int TestItemCount = 1*1000;
            var latch = new CountdownEvent(TestItemCount);
            var listener = new EntryAdapter<object, object>(
                delegate { },
                delegate { latch.Signal(); },
                delegate { },
                delegate { });

            for (var i = 0; i < TestItemCount; i++)
            {
                map.Put("key" + i, new[] {byte.MaxValue});
            }

            while (map.Size() < TestItemCount)
            {
                Thread.Sleep(1000);
            }

            for (var i = 0; i < TestItemCount; i++)
            {
                map.AddEntryListener(listener, "key" + i, false);
            }

            for (var i = 0; i < TestItemCount; i++)
            {
                var o = map.RemoveAsync("key" + i).Result;
            }

            latch.Wait(TimeSpan.FromSeconds(10));
            //Console.WriteLine(latch.CurrentCount);
            latch.Wait(TimeSpan.FromSeconds(10));
            //Console.WriteLine(latch.CurrentCount);
            latch.Wait(TimeSpan.FromSeconds(10));
            //Console.WriteLine(latch.CurrentCount);
            latch.Wait(TimeSpan.FromSeconds(10));
            //Console.WriteLine(latch.CurrentCount);
            latch.Wait(TimeSpan.FromSeconds(10));
            //Console.WriteLine(latch.CurrentCount);
            latch.Wait(TimeSpan.FromSeconds(10));
            //Console.WriteLine(latch.CurrentCount);
            latch.Wait(TimeSpan.FromSeconds(10));
            //Console.WriteLine(latch.CurrentCount);
            Assert.True(latch.Wait(TimeSpan.FromSeconds(100)));
        }

        [Test]
        public void TestListenerPredicate()
        {
            var latch1Add = new CountdownEvent(1);
            var latch1Remove = new CountdownEvent(1);
            var latch2Add = new CountdownEvent(1);
            var latch2Remove = new CountdownEvent(1);
            var listener1 = new EntryAdapter<object, object>(
                delegate { latch1Add.Signal(); },
                delegate { latch1Remove.Signal(); },
                delegate { },
                delegate { });

            var listener2 = new EntryAdapter<object, object>(
                delegate { latch2Add.Signal(); },
                delegate { latch2Remove.Signal(); },
                delegate { },
                delegate { });

            map.AddEntryListener(listener1, new SqlPredicate("this == value1"), false);
            map.AddEntryListener(listener2, new SqlPredicate("this == value3"), "key3", true);

            Thread.Sleep(1000);

            map.Put("key1", "value1");
            map.Put("key2", "value2");
            map.Put("key3", "value3");
            map.Put("key4", "value4");
            map.Put("key5", "value5");

            Thread.Sleep(1000);

            map.Remove("key1");
            map.Remove("key3");

            Assert.IsTrue(latch1Add.Wait(TimeSpan.FromSeconds(10)));
            Assert.IsTrue(latch1Remove.Wait(TimeSpan.FromSeconds(10)));
            Assert.IsTrue(latch2Add.Wait(TimeSpan.FromSeconds(5)));
            Assert.IsTrue(latch2Remove.Wait(TimeSpan.FromSeconds(5)));
        }

        [Test]
        public void testListenerRemove()
        {
            var latch1Add = new CountdownEvent(1);
            var listener1 = new EntryAdapter<object, object>(
                delegate { latch1Add.Signal(); },
                delegate { },
                delegate { },
                delegate { });

            var reg1 = map.AddEntryListener(listener1, false);

            Thread.Sleep(1000);
            Assert.IsTrue(map.RemoveEntryListener(reg1));

            Thread.Sleep(1000);

            map.Put("key1", "value1");

            Thread.Sleep(1000);

            Assert.IsFalse(latch1Add.Wait(TimeSpan.FromSeconds(5)));
        }

        //TODO mapstore
        /// <exception cref="System.Exception"></exception>
        [Test]
        public virtual void TestLock()
        {
            map.Put("key1", "value1");
            Assert.AreEqual("value1", map.Get("key1"));
            map.Lock("key1");
            var latch = new CountdownEvent(1);

            var t1 = new Thread(delegate(object o)
            {
                map.TryPut("key1", "value2", 1, TimeUnit.SECONDS);
                latch.Signal();
            });
            t1.Start();
            Assert.IsTrue(latch.Wait(TimeSpan.FromSeconds(5)));
            Assert.AreEqual("value1", map.Get("key1"));
            map.ForceUnlock("key1");
        }

        /// <exception cref="System.Exception"></exception>
        [Test]
        public virtual void TestLockTtl()
        {
            map.Put("key1", "value1");
            Assert.AreEqual("value1", map.Get("key1"));
            map.Lock("key1", 2, TimeUnit.SECONDS);
            var latch = new CountdownEvent(1);
            var t1 = new Thread(delegate(object o)
            {
                map.TryPut("key1", "value2", 5, TimeUnit.SECONDS);
                latch.Signal();
            });

            t1.Start();
            Assert.IsTrue(latch.Wait(TimeSpan.FromSeconds(10)));
            Assert.IsFalse(map.IsLocked("key1"));
            Assert.AreEqual("value2", map.Get("key1"));
            map.ForceUnlock("key1");
        }

        /// <exception cref="System.Exception"></exception>
        [Test]
        public virtual void TestLockTtl2()
        {
            map.Lock("key1", 3, TimeUnit.SECONDS);
            var latch = new CountdownEvent(2);
            var t1 = new Thread(delegate(object o)
            {
                if (!map.TryLock("key1"))
                {
                    latch.Signal();
                }
                try
                {
                    if (map.TryLock("key1", 5, TimeUnit.SECONDS))
                    {
                        latch.Signal();
                    }
                }
                catch (Exception e)
                {
                }
            });

            t1.Start();
            Assert.IsTrue(latch.Wait(TimeSpan.FromSeconds(10)));
            map.ForceUnlock("key1");
        }

        [Test]
        public virtual void TestPutBigData()
        {
            const int dataSize = 128000;
            var largeString = string.Join(",", Enumerable.Range(0, dataSize));

            map.Put("large_value", largeString);
            Assert.AreEqual(map.Size(), 1);
        }

        /// <exception cref="System.Exception"></exception>
        [Test]
        public virtual void TestPutIfAbsent()
        {
            Assert.IsNull(map.PutIfAbsent("key1", "value1"));
            Assert.AreEqual("value1", map.PutIfAbsent("key1", "value3"));
        }

        [Test]
        public void TestPutIfAbsentNewValueTTL_whenKeyPresent()
        {
            object key = "Key";
            object value = "Value";
            object newValue = "newValue";

            map.Put(key, value);
            var result = map.PutIfAbsent(key, newValue, 5, TimeUnit.MINUTES);

            Assert.AreEqual(value, result);
            Assert.AreEqual(value, map.Get(key));
        }

        ///// <exception cref="System.Exception"></exception>
        //[Test]
        //public virtual void TestPutIfAbsentTtl()
        //{
        //    Assert.IsNull(map.PutIfAbsent("key1", "value1", 1, TimeUnit.SECONDS));
        //    Thread.Sleep(2000);
        //    Assert.AreEqual("value1", map.PutIfAbsent("key1", "value3", 1, TimeUnit.SECONDS));
        //    Thread.Sleep(2000);
        //    Assert.IsNull(map.PutIfAbsent("key1", "value3", 1, TimeUnit.SECONDS));
        //    Assert.AreEqual("value3", map.PutIfAbsent("key1", "value4", 1, TimeUnit.SECONDS));
        //    Thread.Sleep(2000);
        //}

        [Test]
        public void TestPutIfAbsentTtl()
        {
            object key = "Key";
            object value = "Value";

            var result = map.PutIfAbsent(key, value, 5, TimeUnit.MINUTES);

            Assert.AreEqual(null, result);
            Assert.AreEqual(value, map.Get(key));
        }

        [Test]
        public void testPutIfAbsentTTL_whenExpire()
        {
            object key = "Key";
            object value = "Value";

            var result = map.PutIfAbsent(key, value, 1, TimeUnit.SECONDS);
            Thread.Sleep(2000);

            Assert.AreEqual(null, result);
            Assert.AreEqual(null, map.Get(key));
        }

        [Test]
        public void TestPutIfAbsentTTL_whenKeyPresent()
        {
            object key = "Key";
            object value = "Value";

            map.Put(key, value);
            var result = map.PutIfAbsent(key, value, 5, TimeUnit.MINUTES);

            Assert.AreEqual(value, result);
            Assert.AreEqual(value, map.Get(key));
        }

        [Test]
        public void TestPutIfAbsentTTL_whenKeyPresentAfterExpire()
        {
            object key = "Key";
            object value = "Value";

            map.Put(key, value);
            var result = map.PutIfAbsent(key, value, 1, TimeUnit.SECONDS);

            Assert.AreEqual(value, result);
            Assert.AreEqual(value, map.Get(key));
        }

        [Test]
        public virtual void TestPutTransient()
        {
            Assert.AreEqual(0, map.Size());

            map.PutTransient("key1", "value1", 2, TimeUnit.SECONDS);
            Assert.AreEqual("value1", map.Get("key1"));

            Thread.Sleep(TimeSpan.FromSeconds(3));

            Assert.AreNotEqual("value1", map.Get("key1"));
        }

        /// <exception cref="System.Exception"></exception>
        [Test]
        public virtual void TestPutTtl()
        {
            map.Put("key1", "value1", 1, TimeUnit.SECONDS);
            Assert.IsNotNull(map.Get("key1"));
            Thread.Sleep(2000);
            Assert.IsNull(map.Get("key1"));
        }

        [Test]
        public virtual void TestRemoveAndDelete()
        {
            FillMap();
            Assert.IsNull(map.Remove("key10"));
            map.Delete("key9");
            Assert.AreEqual(9, map.Size());
            for (var i = 0; i < 9; i++)
            {
                var o = map.Remove("key" + i);
                Assert.AreEqual("value" + i, o);
            }
            Assert.AreEqual(0, map.Size());
        }

        [Test]
        public virtual void TestRemoveIfSame()
        {
            FillMap();
            Assert.IsFalse(map.Remove("key2", "value"));
            Assert.AreEqual(10, map.Size());
            Assert.IsTrue(map.Remove("key2", "value2"));
            Assert.AreEqual(9, map.Size());
        }

        /// <exception cref="System.Exception"></exception>
        [Test]
        public virtual void TestReplace()
        {
            Assert.IsNull(map.Replace("key1", "value1"));
            map.Put("key1", "value1");
            Assert.AreEqual("value1", map.Replace("key1", "value2"));
            Assert.AreEqual("value2", map.Get("key1"));
            Assert.IsFalse(map.Replace("key1", "value1", "value3"));
            Assert.AreEqual("value2", map.Get("key1"));
            Assert.IsTrue(map.Replace("key1", "value2", "value3"));
            Assert.AreEqual("value3", map.Get("key1"));
        }

        /// <exception cref="System.Exception"></exception>
        [Test]
        public virtual void TestSet()
        {
            map.Set("key1", "value1");
            Assert.AreEqual("value1", map.Get("key1"));
            map.Set("key1", "value2");
            Assert.AreEqual("value2", map.Get("key1"));
            map.Set("key1", "value3", 1, TimeUnit.SECONDS);
            Assert.AreEqual("value3", map.Get("key1"));
            Thread.Sleep(2000);
            Assert.IsNull(map.Get("key1"));
        }

        /// <exception cref="System.Exception"></exception>
        [Test]
        public virtual void TestTryPutRemove()
        {
            Assert.IsTrue(map.TryPut("key1", "value1", 1, TimeUnit.SECONDS));
            Assert.IsTrue(map.TryPut("key2", "value2", 1, TimeUnit.SECONDS));
            map.Lock("key1");
            map.Lock("key2");
            var latch = new CountdownEvent(2);

            var t1 = new Thread(delegate(object o)
            {
                var result = map.TryPut("key1", "value3", 1, TimeUnit.SECONDS);
                if (!result)
                {
                    latch.Signal();
                }
            });

            var t2 = new Thread(delegate(object o)
            {
                var result = map.TryRemove("key2", 1, TimeUnit.SECONDS);
                if (!result)
                {
                    latch.Signal();
                }
            });

            t1.Start();
            t2.Start();

            Assert.IsTrue(latch.Wait(TimeSpan.FromSeconds(20)));
            Assert.AreEqual("value1", map.Get("key1"));
            Assert.AreEqual("value2", map.Get("key2"));

            map.ForceUnlock("key1");
            map.ForceUnlock("key2");
        }

        [Test]
        public virtual void TestUnlock()
        {
            map.ForceUnlock("key1");
            map.Put("key1", "value1");
            Assert.AreEqual("value1", map.Get("key1"));
            map.Lock("key1");
            Assert.IsTrue(map.IsLocked("key1"));
            map.Unlock("key1");
            Assert.IsFalse(map.IsLocked("key1"));
            map.ForceUnlock("key1");
        }

        [Test]
        public virtual void TestValues()
        {
            map.Put("key1", "value1");
            var values = map.Values();
            var enumerator = values.GetEnumerator();
            enumerator.MoveNext();
            Assert.AreEqual("value1", enumerator.Current);
        }

        [Test]
        public void TestValuesPredicate()
        {
            FillMap();

            var values = map.Values(new SqlPredicate("this == value1"));
            Assert.AreEqual(1, values.Count);
            var enumerator = values.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual("value1", enumerator.Current);
        }
    }
}