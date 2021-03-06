// *
// * Copyright (C) 2005 Mats Helander : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System;
using System.Runtime.Serialization;

namespace Puzzle.NPersist.Framework.Exceptions
{
	public class BothCleanMergeException : MergeException
	{
		public BothCleanMergeException() : base()
		{
		}

		public BothCleanMergeException(string message, object obj, string propertyName) : base(message, obj, propertyName)
		{
		}

		public BothCleanMergeException(string message, Exception innerException, object obj, string propertyName) : base(message, innerException, obj, propertyName )
		{
		}

		public BothCleanMergeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public BothCleanMergeException(string message, object cachedValue, object mergeValue, object cached, object merge, string propertyName, bool isOriginal) : base(message, cachedValue, mergeValue, cached, merge, propertyName, isOriginal)
		{
		}		

	}
}
