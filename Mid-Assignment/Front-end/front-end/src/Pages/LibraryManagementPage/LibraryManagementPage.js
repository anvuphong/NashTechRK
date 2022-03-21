import axios from "axios";
import React, { useEffect, useState } from "react";
import { Accordion, Button, Col, Container, Row, Table } from "react-bootstrap";
import AddCompoment from "../../Compoment/AddCompoment/AddCompoment";
import DeleteComponent from "../../Compoment/DeleteComponent/DeleteComponent";
import RequestControlComponet from "../../Compoment/RequestControlComponent/RequestControlComponent";
import UpdateCompoment from "../../Compoment/UpdateCompoment/UpdateCompoment";
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
    const [model, setModel] = useState({
        name: 'None',
        data: null
    });
    const [modalAddShow, setModalAddShow] = useState(false);
    const [modalUpdateShow, setModalUpdateShow] = useState(false);
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
            setModel({
                name: 'Book'
            });
        }
        if (evt.target.id === 'add-category') {
            setModel({
                name: 'Category'
            });
        }
    }

    //Update
    const handleUpdate = (evt) => {
        setModalUpdateShow(true)
        if (evt.target.id === 'update-book') {
            setModel({
                name: 'Book',
                data: books.find(book => book.bookId === Number(evt.target.value))
            });
        }
        if (evt.target.id === 'update-category') {
            setModel({
                name: 'Category',
                data: categories.find(category => category.categoryId === Number(evt.target.value))
            });
        }
    }

    //Delete
    const handleDelete = (evt) => {
        setModalDeleteShow(true)
        if (evt.target.id === 'delete-book') {
            setModel({
                name: 'Book',
                data: books.find(book => book.bookId === Number(evt.target.value))
            });
        }
        if (evt.target.id === 'delete-category') {
            setModel({
                name: 'Category',
                data: categories.find(category => category.categoryId === Number(evt.target.value))
            });
        }
    }

    return (
        <div>
            <h1 style={{ textAlign: 'center', marginBottom:'50px' }}>LibraryManagementPage</h1>
            <AddCompoment
                show={modalAddShow}
                onHide={() => {
                    setModalAddShow(false);
                    setModel({
                        name: 'None'
                    });
                }}
                model={model}
                categories={categories}
            />
            <UpdateCompoment
                show={modalUpdateShow}
                onHide={() => {
                    setModalUpdateShow(false);
                    setModel({
                        name: 'None',
                        data: null
                    });
                }}
                model={model}
                categories={categories}
            />
            <DeleteComponent
                show={modalDeleteShow}
                onHide={() => {
                    setModalDeleteShow(false);
                    setModel({
                        name: 'None',
                        data: null
                    });
                }}
                model={model}
            />

            <Accordion defaultActiveKey={['0']} alwaysOpen>
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
                                    <th style={{ width: '20%' }}>Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                {booksFilter.map(book => (
                                    <tr key={book.bookId}>
                                        <td>{book.bookName}</td>
                                        <td>{book.author}</td>
                                        <td>{getCategoryById(book.categoryId)}</td>
                                        <td>
                                            <Button onClick={handleUpdate} value={book.bookId} style={{ marginRight: '10px' }} variant="outline-warning" id="update-book" >
                                                Update
                                            </Button>
                                            <Button onClick={handleDelete} value={book.bookId} variant="outline-danger" id="delete-book" >
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
                                    <th style={{ width: '70%' }}>Category</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                {categoriesFilter.map(category => (
                                    <tr key={category.categoryId}>
                                        <td>{category.categoryName}</td>
                                        <td>
                                            <Button onClick={handleUpdate} value={category.categoryId} style={{ marginRight: '10px' }} variant="outline-warning" id="update-category" >
                                                Update
                                            </Button>
                                            <Button onClick={handleDelete} value={category.categoryId} variant="outline-danger" id="delete-category" >
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