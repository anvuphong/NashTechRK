import axios from "axios";
import React, { useEffect, useState } from "react";
import { Accordion, Button, Col, Container, Row, Table } from "react-bootstrap";
import AddCompoment from "../../Compoment/AddCompoment/AddCompoment";
import DeleteComponent from "../../Compoment/DeleteComponent/DeleteComponent";
import RequestControlComponet from "../../Compoment/RequestControlComponent/RequestControlComponent";
import './LibraryManagementPage.css'

const LibraryManagementPage = () => {
    const [categories, setCategories] = useState([]);
    const [books, setBooks] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState('');
    const [searchBookName, setSearchBookName] = useState('');
    const [searchAuthor, setSearchAuthor] = useState('');
    const [searchCategory, setSearchCategory] = useState('');
    const [booksFilter, setBooksFilter] = useState([]);
    const [categoriesFilter, setCategoriesFilter] = useState([]);
    const [model, setModel] = useState('None');
    const [modalAddShow, setModalAddShow] = useState(false);
    //const [modalAddShow, setModalAddShow] = useState(false);
    const [modalDeleteShow, setModalDeleteShow] = useState(false);

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
                    setCategoriesFilter(response.data);
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

    //classify category
    const getCategoryById = (categoryId) => {
        const category = categories.find(c => c.categoryId === categoryId);
        return category.categoryName;
    }

    //search keyword
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
        if (evt.target.className === "search-category") {
            setSearchCategory(evt.target.value);
            setCategoriesFilter(categories.filter(category => category.categoryName.toLowerCase().includes(evt.target.value.toLowerCase())))
        }
    }

    //Add
    const handleAdd = (evt) => {
        setModalAddShow(true)
        if (evt.target.id === 'add-book') {
            setModel('Book')
        }
        if (evt.target.id === 'add-category') {
            setModel('Category')
        }
    }
    
    //Delete
    const handleDelete = (evt) => {
        setModalDeleteShow(true)
        if (evt.target.id === 'delete-book') {
            setModel('Book')
        }
        if (evt.target.id === 'delete-category') {
            setModel('Category')
        }
    }

    return (
        <div>
            <h1 style={{ textAlign: 'center' }}>LibraryManagementPage</h1>
            <AddCompoment
                show={modalAddShow}
                onHide={() => {
                    setModalAddShow(false);
                    setModel('None');
                }}
                model={model}
                categories={categories}
            />
            <DeleteComponent
                show={modalDeleteShow}
                onHide={() => {
                    setModalDeleteShow(false);
                    setModel('None');
                }}
                model={model}
                categories={categories}
            />

            <Accordion>
                <Accordion.Item eventKey="0">
                    <Accordion.Header >
                        <Container fluid>
                            <Row>
                                <Col sm={2} style={{ textAlign: 'center' }}><strong>Book</strong></Col>
                                <Col sm={4} style={{ float: 'right' }}>
                                    <input
                                        className="search-book"
                                        placeholder="Search book by title"
                                        value={searchBookName}
                                        onChange={handleSearchChange} />
                                </Col>
                                <Col sm={4}>
                                    <input className="search-author"
                                        placeholder="Search book by author"
                                        value={searchAuthor}
                                        onChange={handleSearchChange} />
                                </Col>
                            </Row>
                        </Container>
                    </Accordion.Header>
                    <Accordion.Body>
                        <Table striped bordered hover className="books-table">
                            <thead>
                                <tr style={{ textAlign: 'center' }}>
                                    <th>Book</th>
                                    <th>Author</th>
                                    <th>Category</th>
                                    <th style={{width:'20%'}}>Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                {booksFilter.map(book => (
                                    <tr key={book.bookId}>
                                        <td>{book.bookName}</td>
                                        <td>{book.author}</td>
                                        <td>{getCategoryById(book.categoryId)}</td>
                                        <td>
                                            <Button style={{ marginRight: '10px' }} variant="outline-warning" id="update-book" >
                                                Update
                                            </Button>
                                            <Button onClick={handleDelete} variant="outline-danger" id="delete-book" >
                                                Delete
                                            </Button>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </Table>
                        <Button variant="outline-primary" id="add-book" onClick={handleAdd}>
                            Add
                        </Button>
                    </Accordion.Body>
                </Accordion.Item>
                <Accordion.Item eventKey="1">
                    <Accordion.Header>
                        <Container fluid>
                            <Row>
                                <Col sm={2} style={{ textAlign: 'center' }}><strong>Category</strong></Col>
                                <Col sm={8} style={{ float: 'right' }}>
                                    <input
                                        className="search-category"
                                        placeholder="Search category"
                                        value={searchCategory}
                                        onChange={handleSearchChange} />
                                </Col>
                            </Row>
                        </Container>
                    </Accordion.Header>
                    <Accordion.Body>
                        <Table striped bordered hover className="categories-table">
                            <thead>
                                <tr>
                                    <th style={{width:'70%'}}>Category</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                {categoriesFilter.map(category => (
                                    <tr key={category.categoryId}>
                                        <td>{category.categoryName}</td>
                                        <td>
                                            <Button style={{ marginRight: '10px' }} variant="outline-warning" id="update-category" >
                                                Update
                                            </Button>
                                            <Button onClick={handleDelete} variant="outline-danger" id="delete-category" >
                                                Delete
                                            </Button>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </Table>
                        <Button variant="outline-primary" id="add-category" onClick={handleAdd}>
                            Add
                        </Button>
                    </Accordion.Body>
                </Accordion.Item>
                <Accordion.Item eventKey="2">
                    <Accordion.Header>
                        <Container fluid>
                            <Row>
                                <Col sm={2} style={{ textAlign: 'center' }}><strong>Request</strong></Col>
                            </Row>
                        </Container>
                    </Accordion.Header>
                    <Accordion.Body>
                        {/* <Table striped bordered hover className="categories-table">
                            <thead>
                                <tr>
                                    <th>Category</th>
                                    <th>Preview</th>
                                </tr>
                            </thead>
                            <tbody>
                                {categoriesFilter.map(category => (
                                    <tr key={category.categoryId}>
                                        <td>{category.categoryName}</td>
                                    </tr>
                                ))}
                            </tbody>
                        </Table> */}
                        <RequestControlComponet />
                        <Button variant="outline-primary" id="add-category" onClick={handleAdd}>
                            Add
                        </Button>
                    </Accordion.Body>
                </Accordion.Item>
            </Accordion>
        </div>
    )
}
export default LibraryManagementPage;