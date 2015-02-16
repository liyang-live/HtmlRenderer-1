﻿//BSD  2014 ,WinterDev  
using LayoutFarm.HtmlBoxes;
using System.Collections.Generic;

namespace LayoutFarm.WebDom
{

    public abstract class WebDocument
    {
        UniqueStringTable uniqueStringTable;
        Dictionary<string, DomElement> registerElementsById;

        public WebDocument(UniqueStringTable uniqueStringTable)
        {
            this.uniqueStringTable = uniqueStringTable;
            this.DocumentState = WebDom.DocumentState.Init;
        }
        public abstract DomElement RootNode
        {
            get;
            set;
        }
        public abstract int DomUpdateVersion { get; set; }
        public int AddStringIfNotExists(string uniqueString)
        {
            return uniqueStringTable.AddStringIfNotExist(uniqueString);
        }
        public string GetString(int index)
        {
            return uniqueStringTable.GetString(index);
        }
        public int FindStringIndex(string uniqueString)
        {
            return uniqueStringTable.GetStringIndex(uniqueString);
        }

        public DomAttribute CreateAttribute(string prefix, string localName)
        {
            return new DomAttribute(this,
                uniqueStringTable.AddStringIfNotExist(prefix),
                uniqueStringTable.AddStringIfNotExist(localName));
        }
        public abstract DomElement CreateElement(string prefix, string localName);

        public DomElement CreateElement(string localName)
        {
            return this.CreateElement(null, localName);
        }

        public DomComment CreateComent()
        {
            return new DomComment(this);
        }
        public DomProcessInstructionNode CreateProcessInstructionNode(int nameIndex)
        {
            return new DomProcessInstructionNode(this, nameIndex);
        }

        public abstract DomTextNode CreateTextNode(char[] strBufferForElement);

        public DomCDataNode CreateCDataNode()
        {
            return new DomCDataNode(this);
        }
        //-------------------------------------------------------
        internal void RegisterElementById(DomElement element)
        {
            if (registerElementsById == null) this.registerElementsById = new Dictionary<string, DomElement>();

            //replace exisitng if exists *** 
            registerElementsById[element.AttrElementId] = element;
        }
        //-------------------------------------------------------
        public DocumentState DocumentState
        {
            get;
            private set;
        }
        public void SetDocumentState(DocumentState docstate)
        {
            this.DocumentState = docstate;
        }
        internal UniqueStringTable UniqueStringTable
        {
            get { return this.uniqueStringTable; }
        }
    }

    public enum DocumentState
    {
        Init,
        Building,
        Idle,
        ChangedAfterIdle
    }

    public class WebDocumentFragment
    {
        WebDocument owner;
        public WebDocumentFragment(WebDocument owner)
        {
            this.owner = owner;
        }
    }

}