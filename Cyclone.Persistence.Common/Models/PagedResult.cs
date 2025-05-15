using System;
using System.Collections.Generic;
using System.Linq;
using Cyclone.Persistence.Abstractions.Query;

namespace Cyclone.Persistence.Common.Models
{
    /// <summary>
    /// ��ʾ��ҳ��ѯ���
    /// </summary>
    /// <typeparam name="T">ʵ������</typeparam>
    public class PagedResult<T> : IPaginatedResult<T> where T : class
    {
        /// <summary>
        /// ��ȡ��ǰҳ��
        /// </summary>
        public int PageNumber { get; private set; }

        /// <summary>
        /// ��ȡҳ���С
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// ��ȡ�ܼ�¼��
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// ��ȡ��ҳ��
        /// </summary>
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        /// <summary>
        /// ��ȡ�Ƿ�����һҳ
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// ��ȡ�Ƿ�����һҳ
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;

        /// <summary>
        /// ��ȡ��ǰҳ����
        /// </summary>
        public IEnumerable<T> Items { get; private set; }

        /// <summary>
        /// ��ʼ����ҳ���
        /// </summary>
        public PagedResult()
        {
            Items = Enumerable.Empty<T>();
        }

        /// <summary>
        /// ��ʼ����ҳ���
        /// </summary>
        /// <param name="items">��ǰҳ����</param>
        /// <param name="totalCount">�ܼ�¼��</param>
        /// <param name="pageNumber">��ǰҳ��</param>
        /// <param name="pageSize">ҳ���С</param>
        public PagedResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "ҳ�������ڻ����1��");

            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "ҳ���С������ڻ����1��");

            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            Items = items ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// �����յķ�ҳ���
        /// </summary>
        /// <param name="pageNumber">��ǰҳ��</param>
        /// <param name="pageSize">ҳ���С</param>
        /// <returns>�յķ�ҳ���</returns>
        public static PagedResult<T> Empty(int pageNumber, int pageSize)
        {
            return new PagedResult<T>(Enumerable.Empty<T>(), 0, pageNumber, pageSize);
        }

        /// <summary>
        /// �����յķ�ҳ���
        /// </summary>
        /// <returns>�յķ�ҳ���</returns>
        public static PagedResult<T> Empty()
        {
            return Empty(1, 10);
        }

        /// <summary>
        /// �Ӽ��ϴ�����ҳ���
        /// </summary>
        /// <param name="source">Դ����</param>
        /// <param name="pageNumber">��ǰҳ��</param>
        /// <param name="pageSize">ҳ���С</param>
        /// <returns>��ҳ���</returns>
        public static PagedResult<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var enumerable = source as T[] ?? source.ToArray();
            var count = enumerable.Length;
            var items = enumerable.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<T>(items, count, pageNumber, pageSize);
        }
    }
}