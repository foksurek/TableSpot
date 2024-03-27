import SearchIcon from '@mui/icons-material/Search';

type Props = {
    onClick?: () => void;

}

const SearchBarPlaceholder = (props: Props) => {
    return (
        <>
            <div className="searchBarPlaceholder" onClick={props.onClick}>
                <SearchIcon/>
                <div>Search for a business</div>
            </div>
        </>
    )
}

export default SearchBarPlaceholder;