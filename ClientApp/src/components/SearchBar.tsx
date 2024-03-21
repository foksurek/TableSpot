import SearchIcon from '@mui/icons-material/Search';

const SearchBar = () => {
    return (
        <>
            <div className="searchBar">
                <SearchIcon/>
                <input type="text" placeholder="Search for a business"/>
            </div>
        </>
    )
}

export default SearchBar;