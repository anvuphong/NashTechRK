import axios from "axios";
import React, { useEffect, useState } from "react";
import { Alert, Button, Container, Dropdown, DropdownButton, Form, Table } from "react-bootstrap";
import { Link } from "react-router-dom";

const BookListPage = () => {
    const [categories, setCategories] = useState([]);
    const [books, setBooks] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState('');
    const [searchBookName, setSearchBookName] = useState('');
    const [searchAuthor, setSearchAuthor] = useState('');
    const [booksFilter, setBooksFilter] = useState([]);
    const [sortByTitle, setSortByTitle] = useState('ASC');
    const [requestList, setRequestList] = useState([]);
    const [showAlert, setShowAlert] = useState('NONE');

    //API
    useEffect(() => {
        (async () => {
            let isCancel = false;
            //get category
            await axios({
                method: 'GET',
                url: 'https://localhost:7279/api/category'
            }).then(response => {
                if (!isCancel) {
                    setCategories(response.data);
                    setIsLoading(false);
                }
            }).catch((error) => {
                if (!isCancel) {
                    setIsLoading(false);
                    setError('Something wrong');
                }
            });

            //get books
            await axios({
                method: 'GET',
                url: 'https://localhost:7279/api/book'
            }).then(response => {
                if (!isCancel) {
                    setBooks(response.data);
                    setBooksFilter(response.data);
                    setIsLoading(false);
                }
            }).catch((error) => {
                if (!isCancel) {
                    setIsLoading(false);
                    setError('Something wrong');
                }
            });
            return () => {
                isCancel = true;
            }
        })()
    }, []);

    //Status Request
    if (isLoading) return (
        <div>Loading</div>
    );
    if (error) return (
        <div style={{ color: 'red' }}>{error}</div>
    );

    const handleSearchChange = (evt) => {
        if (evt.target.className === "search-book") {
            setSearchBookName(evt.target.value);
            setSearchAuthor('');
            setBooksFilter(books.filter(book => book.bookName.toLowerCase().includes(evt.target.value.toLowerCase())))
        }
        if (evt.target.className === "search-author") {
            setSearchAuthor(evt.target.value);
            setSearchBookName('');
            setBooksFilter(books.filter(book => book.author.toLowerCase().includes(evt.target.value.toLowerCase())))
        }
        if (evt.target.className === "dropdown-item") {
            setSearchAuthor('');
            setSearchBookName('');
            setBooksFilter(books.filter(book => book.categoryId === Number(evt.target.id)))
        }
        if (evt.target.className === "all dropdown-item") {
            setSearchAuthor('');
            setSearchBookName('');
            setBooksFilter(books);
        }
    }

    //Books sort
    const getbooksSorted = () => {
        switch (sortByTitle) {
            case 'ASC': return booksFilter.sort((book1, book2) => {
                if (book1.bookName.toLowerCase() < book2.bookName.toLowerCase()) return -1;
                if (book1.bookName.toLowerCase() > book2.bookName.toLowerCase()) return 1;
            })
            case 'DES': return booksFilter.sort((book1, book2) => {
                if (book1.bookName.toLowerCase() > book2.bookName.toLowerCase()) return -1;
                if (book1.bookName.toLowerCase() < book2.bookName.toLowerCase()) return 1;
            })
        }
    }
    const booksSorted = getbooksSorted();

    const handleChangeSortByTitle = () => {
        if (sortByTitle === 'ASC') {
            setSortByTitle('DES');
            return;
        }
        if (sortByTitle === 'DES') {
            setSortByTitle('ASC');
            return;
        }
    }

    //classify category
    const getCategoryById = (categoryId) => {
        const category = categories.find(c => c.categoryId === categoryId);
        return category.categoryName;
    }

    //Submit Request
    const handleCLick = (evt) => {
        if (evt.target.checked) {
            setRequestList(
                [...requestList, Number(evt.target.value)]
            );
        } else {
            setRequestList(requestList.filter(request => request !== Number(evt.target.value)))
        }
    }

    const handleSubmitRequest = () => {
        if (requestList.length > 5) {
            setShowAlert('ERROR');
        }else if (requestList.length !== 0) {
            (async () => {
                let isCancel = false;
                //get category
                await axios({
                    method: 'POST',
                    url: 'https://localhost:7279/api/book-request',
                    data: {
                        requestByUserId: '3',
                        dateOfRequest: "2022-03-20T06:25:17.858Z",
                        status: 0,
                        listBookId: requestList
                    }
                }).catch((error) => {
                    if (!isCancel) {
                        setIsLoading(false);
                        setError('Something wrong');
                    }
                });
                return () => {
                    isCancel = true;
                }
            })();
            setRequestList([]);
            setShowAlert('SUCCESS');
        }
    }

    return (
        <div style={{ margin: "auto" }}>
            <h1 style={{ textAlign: 'center' }}>Book List</h1>
            <div className="row" style={{ margin: "20px" }}>
                <div className="col">
                    <input className="search-book"
                        placeholder="Search book by title"
                        value={searchBookName}
                        onChange={handleSearchChange} />
                </div>
                <div className="col">
                    <input className="search-author"
                        placeholder="Search book by author"
                        value={searchAuthor}
                        onChange={handleSearchChange} />
                </div>
                <div className="col">
                    <DropdownButton id="dropdown-basic-button" title="Choose a Category">
                        <Dropdown.Item className="all" onClick={handleSearchChange}>All</Dropdown.Item>
                        {categories.map(category => (
                            <Dropdown.Item id={category.categoryId} key={category.categoryId} onClick={handleSearchChange}>
                                {category.categoryName}
                            </Dropdown.Item>
                        ))}
                    </DropdownButton>
                </div>
            </div>
            <Container fluid>
                <Table striped bordered hover className="books-table">
                    <thead>
                        <tr>
                            <th onClick={handleChangeSortByTitle}>Book</th>
                            <th>Author</th>
                            <th>Category</th>
                            <th>Preview</th>
                        </tr>
                    </thead>
                    <tbody>
                        {booksSorted.map(book => (
                            <tr key={book.bookId}>
                                <td>{book.bookName}</td>
                                <td style={{ textAlign: "left" }}>{book.author}</td>
                                <td style={{ textAlign: "left" }}>{getCategoryById(book.categoryId)}</td>
                                <td>
                                    <Link to={`/book/${book.bookId}`} >View Detail</Link>
                                </td>
                                <td>
                                    <Form.Check checked={Boolean(requestList.find(r => r === book.bookId))} value={book.bookId} onChange={handleCLick} />
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
                <Button variant="primary" onClick={handleSubmitRequest}>
                    Submit Request
                </Button>
            </Container>
            {(showAlert === 'ERROR') &&
                <Alert variant="warning" >
                    <Alert.Heading style={{ textAlign: 'center' }}>You can only request 5 books at a time!</Alert.Heading>
                    <div className="d-flex justify-content-end">
                        <Button onClick={() => setShowAlert('NONE')} variant="outline-success">
                            Close
                        </Button>
                    </div>
                </Alert>}
            {(showAlert === 'SUCCESS') &&
                <Alert variant="success" >
                    <Alert.Heading style={{ textAlign: 'center' }}>Request success</Alert.Heading>
                    <div className="d-flex justify-content-end">
                        <Button onClick={() => setShowAlert('NONE')} variant="outline-success">
                            Close
                        </Button>
                    </div>
                </Alert>}
        </div>
    )
}
export default BookListPage;