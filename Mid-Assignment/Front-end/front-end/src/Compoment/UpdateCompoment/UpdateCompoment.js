import axios from "axios";
import React, { useState } from "react";
import { Button, Form, Modal } from "react-bootstrap";

const UpdateCompoment = (props) => {
    const [data, setData] = useState({
        name: '',
        author: '',
        category: '0'
    });

    const setBaseData = () => {
        if (props.model.name === 'Book') {
            setData({
                name: props.model.data.bookName,
                author: props.model.data.author,
                category: props.model.data.categoryId
            });
        }
        if (props.model.name === 'Category') {
            setData({
                name: props.model.data.categoryName
            })
        }
    }

    const handleInputChange = (evt) => {
        setData({
            ...data,
            [evt.target.name]: evt.target.value
        })
    }

    const handleSubmit = (evt) => {
        evt.preventDefault();
        props.onHide();
        if (props.model.name === 'Book') {
            (async () => {
                let isCancel = false;
                await axios({
                    method: 'PUT',
                    url: 'https://localhost:7279/api/book',
                    data: {
                        bookName: data.name,
                        author: data.author,
                        categoryId: data.category,
                        bookId: props.model.data.bookId
                    }
                })
                return () => {
                    isCancel = true;
                }
            })();
        }
        if (props.model.name === 'Category') {
            (async () => {
                let isCancel = false;
                await axios({
                    method: 'PUT',
                    url: 'https://localhost:7279/api/category',
                    data: {
                        categoryName: data.name,
                        categoryId: props.model.data.categoryId
                    }
                })
                return () => {
                    isCancel = true;
                }
            })();
        }
        window.location.reload();
    }

    return (
        <Modal
            {...props}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
            onShow={setBaseData}
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Update {props.model.name}
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <h4>{props.model.name} Model</h4>
                <Form onSubmit={handleSubmit}>
                    <Form.Group className="update" id="inputName">
                        <Form.Label>{props.model.name}</Form.Label>
                        <Form.Control name="name" type="text" onChange={handleInputChange} value={data.name} />
                        <Form.Text muted>
                            Enter {props.model.name}.
                        </Form.Text>
                    </Form.Group>

                    {(props.model.name === 'Book') &&
                        <Form.Group className="update" id="inputAuthor">
                            <Form.Label>Author</Form.Label>
                            <Form.Control name="author" type="text" onChange={handleInputChange} value={data.author} />
                            <Form.Text muted>
                                Enter Author.
                            </Form.Text>
                        </Form.Group>
                    }
                    {(props.model.name === 'Book') &&
                        <Form.Select value={Number(data.category)} name="category" onChange={handleInputChange} className="update" id="inputCategory">
                            <option value={0}>Choose a Category</option>
                            {props.categories.map(category => (
                                <option key={category.categoryId} value={category.categoryId}>{category.categoryName}</option>
                            ))}
                        </Form.Select>
                    }
                    {(props.model.name === 'Category' && data.name !== '') && <Button style={{ marginTop: '20px' }} type="submit">Confirm</Button>}
                    {(props.model.name === 'Book' && data.name !== '' && data.author !== '' && data.category !== '0') &&
                        <Button style={{ marginTop: '20px' }} type="submit">Confirm</Button>
                    }
                </Form>
            </Modal.Body>
        </Modal>
    )
}
export default UpdateCompoment;