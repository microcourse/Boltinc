import React, { Component } from 'react';

export class SearchResult extends Component {
    displayName = SearchResult.name
    searchEngineMap = {
        1: "Google",
        2: "Bing"
    };

    constructor(props) {
        super(props);
    }

    renderResult(items, bookmarkEnabled) {
        return (
            <div>
                <h2>Total: {items.length}</h2>
                <table className='table'>
                    <thead>
                        <tr>
                            <th>Search Engine</th>
                            <th>Title</th>
                            <th>Entered Date</th>
                        </tr>
                    </thead>
                    <tbody>
                        {items.map(item =>
                            <tr key={item.id}>
                                <td>{this.searchEngineMap[item.searchEngine]  || "Undefined"}</td>
                                <td>{item.title}</td>
                                <td>{item.enteredDate}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }
    
    render() {
        let props = this.props;
        let bookMarkEnabled = props.bookMarkEnabled;

        let contents = props.items && props.items.length <= 0 ?
            <h3>No results</h3> :
            this.renderResult(props.items, bookMarkEnabled);

        return (
            <div>
                {contents}
            </div>
        );
    }
}

SearchResult.defaultProps = {
    items: [],
}

