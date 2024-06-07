const Pagination = (props) => {
    const {itemsPerPage, length, handlePagination, currentPage} = props;
    const paginationNumbers = [];

    for (let i = 1; i <= Math.ceil(length / itemsPerPage); i++) {
        paginationNumbers.push(i);
    }

    return (
        <div className="">
            {paginationNumbers.map((pageNumber) => (
                <button 
                    className={`px-2 py-1 border ${pageNumber === currentPage ? 'bg-slate-300' : 'hover:opacity-50'}`}
                    key={pageNumber}
                    onClick={() => {handlePagination(pageNumber)}}
                    disabled={pageNumber === currentPage}
                >
                    {pageNumber}
                </button>
            ))}
        </div>
    );
};

export default Pagination;