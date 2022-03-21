import axios from "axios";
import React from "react";
import { Button, Modal } from "react-bootstrap";

const DeleteComponent = (props) => {

    const handleConfirm = () => {
        props.onHide();
        if (props.model.name === 'Book') {
            (async () => {
                let isCancel = false;
                await axios({
                    method: 'Delete',
                    url: `https://localhost:7279/api/book?id=${props.model.data.bookId}`,
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
                    method: 'Delete',
                    url: `https://localhost:7279/api/category?id=${props.model.data.categoryId}`,
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
            backdrop="static"
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Delete a {props.model.name}
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <h5>Are you sure?</h5>
                <p>
                    {props.model.name} will be removed from the database
                </p>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="outline-danger" onClick={handleConfirm}>Confirm</Button>
                <Button variant="outline-primary" onClick={props.onHide}>Close</Button>
            </Modal.Footer>
        </Modal>
    )
}
export default DeleteComponent;