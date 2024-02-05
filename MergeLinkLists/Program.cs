var list = new List<List<int>>
{
    new() { 1, 2, 4},
    new() { 1, 3, 4},
    new() { }
};

var listLinkList = new List<LinkList>();
foreach (var tmpList in list)
{
    listLinkList = [.. listLinkList, LinkList.MakeLinkList(tmpList)!];
}

var mergedList = LinkList.MergeListLinkLists(listLinkList!);

foreach (var linkList in listLinkList)
{
    if (linkList is not null)
        linkList.Print();
}
mergedList.Print();





public class LinkList
{
    public int Value { get; set; }
    public LinkList? Next { get; set; }

    public static LinkList? MakeLinkList(List<int> list)
    {
        LinkList? linkList = null;
        for (int i = list.Count - 1; i >= 0; i--)
        {
            linkList = new LinkList
            {
                Value = list[i],
                Next = linkList
            };
        }

        return linkList;
    }

    public static LinkList MergeLinkLists(LinkList? linkList1, LinkList? linkList2)
    {
        if (linkList1 == null)
        {
            return linkList2 ?? new LinkList();
        }
        if (linkList2 == null)
        {
            return linkList1 ?? new LinkList();
        }

        LinkList? linkList;

        if (linkList1.Value <= linkList2.Value)
        {
            linkList = new LinkList
            {
                Value = linkList1.Value,
                Next = MergeLinkLists(linkList1.Next, linkList2)
            };
        }
        else
        {
            linkList = new LinkList
            {
                Value = linkList2.Value,
                Next = MergeLinkLists(linkList1, linkList2.Next)
            };
        }

        return linkList ?? new LinkList();
    }
    
    public static LinkList MergeListLinkLists(List<LinkList?> listLinkList)
    {
        listLinkList = listLinkList.Where(x => x is not null).ToList();

        if (listLinkList.Count == 1)
        {
            return listLinkList[0] ?? new LinkList();
        }

        LinkList? linkList;

        var minIndex = FindMin(listLinkList!);

        var tmpValue = listLinkList[minIndex]!.Value;
        listLinkList[minIndex] = listLinkList[minIndex]!.Next;

        linkList = new LinkList
        {
            Value = tmpValue,
            Next = MergeListLinkLists(listLinkList)
        };

        return linkList ?? new LinkList();
    }

    private static int FindMin(List<LinkList> listLinkList)
    {
        var min = int.MaxValue;
        var index = -1;

        for (int i = 0; i < listLinkList.Count; i++)
        {
            if (listLinkList[i].Value < min)
            {
                min = listLinkList[i].Value;
                index = i;
            }
        }

        return index;
    }

    public void Print()
    {
        var linkList = this;
        while (linkList is not null)
        {
            Console.Write($"{linkList.Value} - ");
            linkList = linkList.Next;
        }
        Console.WriteLine();
    }
}